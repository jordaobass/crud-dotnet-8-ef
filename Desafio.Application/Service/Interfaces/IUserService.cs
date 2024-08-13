using Desafio.Application.domain;
using Desafio.Application.PayLoad.Request;
using Desafio.Application.PayLoad.Response;

namespace Desafio.Application.Service.Interfaces;

public interface IUserService
{
    Task<UserResponse> Save(NewUserRequest request);
    Task<List<UserResponse>> GetAll();
    Task<UserResponse> Get(int id);
}