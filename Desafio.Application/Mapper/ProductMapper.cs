using AutoMapper;
using Desafio.Application.domain;
using Desafio.Application.PayLoad.Request;
using Desafio.Application.PayLoad.Response;

namespace Desafio.Application.Mapper;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<NewProductRequest, Product>();
        CreateMap<Product, ProductResponse>();
    }
}