using AutoMapper;
using Desafio.Application.domain;
using Desafio.Application.PayLoad.Request;
using Desafio.Application.PayLoad.Response;

namespace Desafio.Application.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UserResponse, User>();
        CreateMap<User, UserResponse>();
        CreateMap<NewUserRequest, User>();
    }
}