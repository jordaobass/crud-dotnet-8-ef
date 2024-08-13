using Desafio.Application.domain;
using Desafio.Application.Repository.Context;
using Desafio.Application.Repository.Interfaces;

namespace Desafio.Application.Repository;

public class SaleRespository : RepositoryBase<Sale>, ISaleRespository
{
    public SaleRespository(DesafioContext desafioContext) : base(desafioContext)
    {
    }
}