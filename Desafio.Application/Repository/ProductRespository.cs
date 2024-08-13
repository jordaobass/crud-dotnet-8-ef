using Desafio.Application.domain;
using Desafio.Application.Repository.Context;
using Desafio.Application.Repository.Interfaces;

namespace Desafio.Application.Repository;

public class ProductRespository : RepositoryBase<Product>, IProductRespository
{
    public ProductRespository(DesafioContext desafioContext) : base(desafioContext)
    {
    }
}