using AutoMapper;
using Desafio.Application.domain;
using Desafio.Application.PayLoad.Request;
using Desafio.Application.PayLoad.Response;
using Desafio.Application.Repository.Interfaces;
using Desafio.Application.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Application.Service;

public class ProductService : IProductService
{
    private IProductRespository _repository;
    private IMapper _mapper;

    public ProductService(IProductRespository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<ProductResponse> Save(NewProductRequest request)
    {
        var product = _mapper.Map<Product>(request);
        await _repository.AddAsync(product);
        var response = _mapper.Map<ProductResponse>(product);

        return response;
    }

    public async Task<List<ProductResponse>> GetAll()
    {
        var result = await _repository.GetAll().ToListAsync();
        var list = new List<ProductResponse>();

        foreach (var item in result)
        {
            list.Add(_mapper.Map<ProductResponse>(item));
        }

        return list;
    }

    public async Task<List<Product>> GetBy(List<int> idList)
    {
        var result = await _repository.GetAll()
            .Where(s => idList.Contains(s.Id)).ToListAsync();

        return result;
    }

}