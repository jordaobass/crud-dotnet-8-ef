using AutoMapper;
using Desafio.Application.domain;
using Desafio.Application.exception;
using Desafio.Application.PayLoad.Request;
using Desafio.Application.PayLoad.Response;
using Desafio.Application.Repository.Interfaces;
using Desafio.Application.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Application.Service;

public class SaleService : ISaleService
{
    private ISaleRespository _respository;
    private readonly IUserService _userService;
    private readonly IProductService _productService;
    private IMapper _mapper;

    public SaleService(ISaleRespository saleRespository, IMapper mapper,
        IUserService userService, IProductService productService)
    {
        _respository = saleRespository;
        _mapper = mapper;

        _userService = userService;
        _productService = productService;
    }


    public async Task<SaleResponse> Save(NewSaleRequest request)
    {
        var user = await GetUser(request.UserId);

        var listProducts = await GetListProducts(request.ListProductId);

        var saleProducts = new List<SaleProduct>();
        foreach (var item in listProducts)
        {
            saleProducts.Add(new SaleProduct() { ProductId = item.Id });
        }

        var sale = new Sale()
        {
            SaleProducts = saleProducts,
            UserId = user.Id,
        };

        var result = await _respository.AddAsync(sale);

        var response = PreencherProdutoResponse(result);

        return response;
    }

    public async Task<SaleResponse> ChangeStatus(int saleId, int idStatus)
    {
        var sale =  await GetById(saleId);
        if (!sale.SaleCanBeChange()) throw new DomainException("Venda nao pode ser alterada");

        var value = (EnumStatusSale)idStatus;
      
        sale.ChangeState(value);
        _respository.UpdateAsync(sale);
        var response = PreencherProdutoResponse(sale);

        return response;
    }

    public async Task<SaleResponse> AddItem(UpdateSaleRequest request)
    {
        var sale =  await GetById(request.SaleId);

        if (sale.SaleCanBeChange()) throw new DomainException("Venda nao pode ser alterada");

        await GetListProducts([request.ProductId]);


        if (sale.SaleProducts.Any(s => s.ProductId == request.ProductId))
            throw new DomainException("o item ja esta presente na venda");

        sale.AddItem(request.ProductId);

        _respository.UpdateAsync(sale);

        var response = PreencherProdutoResponse(sale);

        return response;
    }

    public async Task<SaleResponse> RemoveItem(int saleId, int productId)
    {
        var sale =  await GetById(saleId);

        if (sale == null) throw new DomainException("Venda Invalida");
        
        if (sale.SaleCanBeChange()) throw new DomainException("Venda nao pode ser alterada");

        var itemForRemove = sale.SaleProducts.FirstOrDefault(s => s.ProductId == productId);

        if (itemForRemove == null) throw new DomainException("Produto Invalida");

        sale.SaleProducts.Remove(itemForRemove);

        _respository.UpdateAsync(sale);

        var response = PreencherProdutoResponse(sale);
        return response;
    }

    public async Task<List<SaleResponse>> GetAll()
    {
        var result = await _respository.GetAll()
            .Include(sale => sale.User)
            .Include(sale => sale.SaleProducts)
            .ThenInclude(sp => sp.Product)
            .ToListAsync();
        var list = ConvertSaleResponses(result);

        return list;
    }

    public async Task<SaleResponse> Get(int saleId)
    {
        var result =  await GetById(saleId);

        var response = ConvertSaleResponses(new List<Sale>() { result });

        return response.FirstOrDefault();
    }

    private async Task<List<Product>> GetListProducts(List<int> listProductId)
    {
        var listProducts = await _productService.GetBy(listProductId);

        if (listProducts.Count != listProductId.Count)
            throw new DomainException("Nem todos os produtos selecionados sao validos, verifique e tente novamente ");
        return listProducts;
    }

    private async Task<UserResponse> GetUser(int userId)
    {
        var user = await _userService.Get(userId);

        if (user == null)
            throw new DomainException("Vendedor Invalido, verifique os dados e tente novamente  ");
        return user;
    }

    private SaleResponse PreencherProdutoResponse(Sale sale)
    {
        var list = new List<ProductResponse>();

        foreach (var item in sale.SaleProducts)
        {
            list.Add(_mapper.Map<ProductResponse>(item.Product));
        }

        var response = new SaleResponse()
        {
            User = _mapper.Map<UserResponse>(sale.User),
            Product = list,
            Status = sale.Status
        };

        return response;
    }

    private List<ProductResponse> ConvertToList(List<SaleProduct> listProduct)
    {
        var list = new List<ProductResponse>();

        foreach (var item in listProduct)
        {
            list.Add(_mapper.Map<ProductResponse>(item.Product));
        }

        return list;
    }

    private List<SaleResponse> ConvertSaleResponses(List<Sale> result)
    {
        var list = new List<SaleResponse>();

        foreach (var item in result)
        {
            list.Add(new SaleResponse()
            {
                User = _mapper.Map<UserResponse>(item.User),
                Product = ConvertToList(item.SaleProducts.ToList()),
                CreatedAt = item.CreatedAt,
                Id = item.Id
            });
        }

        return list;
    }

    private async Task<Sale> GetById(int id)
    {
        var sale = await _respository.GetAll()
            .Include(sale => sale.User)
            .Include(sale => sale.SaleProducts)
            .ThenInclude(sp => sp.Product)
            .Where(s => s.Id == id).FirstAsync();

        return sale;
    }

}