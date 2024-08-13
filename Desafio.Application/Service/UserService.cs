using AutoMapper;
using Desafio.Application.domain;
using Desafio.Application.PayLoad.Request;
using Desafio.Application.PayLoad.Response;
using Desafio.Application.Repository.Interfaces;
using Desafio.Application.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Application.Service;

public class UserService : IUserService
{
    private IUserRepository _repository;
    private IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserResponse> Save(NewUserRequest request)
    {
        var user = _mapper.Map<User>(request);
        await _repository.AddAsync(user);
        var response = _mapper.Map<UserResponse>(user);

        return response;
    }

    public async Task<List<UserResponse>> GetAll()
    {
        var result = await _repository.GetAll().ToListAsync();
        var list = new List<UserResponse>();

        foreach (var item in result)
        {
            list.Add(_mapper.Map<UserResponse>(item));
        }

        return list;
    }

    public async Task<UserResponse> Get(int id)
    {
        var result = await _repository.GetAll().FirstAsync(user => user.Id == id);

        return _mapper.Map<UserResponse>(result);
    }
}