using Desafio.Application.PayLoad.Request;
using Desafio.Application.PayLoad.Response;

namespace Desafio.Application.Service.Interfaces;

public interface ISaleService
{
    Task<SaleResponse> Save(NewSaleRequest request);
    Task<List<SaleResponse>> GetAll();
    Task<SaleResponse> Get(int id);
    Task<SaleResponse> AddItem(UpdateSaleRequest request);
    Task<SaleResponse> ChangeStatus(int idSale, int idStatus);
    Task<SaleResponse> RemoveItem(int saleId, int productId);
}