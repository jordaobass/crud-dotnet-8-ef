using Desafio.Application.domain;
using Desafio.Application.Repository.Context;
using Desafio.Application.Repository.Interfaces;

namespace Desafio.Application.Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(DesafioContext desafioContext) : base(desafioContext)
    {
    }
}