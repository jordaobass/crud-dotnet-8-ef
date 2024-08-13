using Desafio.Application.domain;
using Desafio.Application.PayLoad.Request;
using Desafio.Application.PayLoad.Response;

namespace Desafio.Application.Service.Interfaces;

public interface IProductService
{
    Task<ProductResponse> Save(NewProductRequest request);
    Task<List<ProductResponse>> GetAll();
    Task<List<Product>> GetBy(List<int> idList);
}