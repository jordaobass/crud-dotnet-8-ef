using AutoMapper;
using Desafio.Application.domain;
using Desafio.Application.PayLoad.Response;

namespace Desafio.Application.Mapper;

public class SaleMapper : Profile
{
    public SaleMapper()
    {
        CreateMap<Sale, SaleResponse>();
 
    }
}