using AutoMapper;
using Desafio.Api.Controllers;
using Desafio.Application.domain;
using Desafio.Application.Mapper;
using Desafio.Application.PayLoad.Request;
using Desafio.Application.PayLoad.Response;
using Desafio.Application.Repository;
using Desafio.Application.Repository.Context;
using Desafio.Application.Repository.Interfaces;
using Desafio.Application.Service;
using Desafio.Application.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Desafio.Tests;

public class UserTest
{
    private readonly Mock<IUserRepository> _mockRepository;

    private readonly Mock<Mapper> _mockMapper;

    // private readonly Mock<IUserService> _mockService;
    private readonly IUserService _mockService;

    private readonly UserController _mockController;
    private readonly Mock<DbSet<User>> _mockSet;
    private readonly Mock<DesafioContext> _mockContext;

    private readonly IMapper _mapper;

    public UserTest()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new UserMapper());
            mc.AddProfile(new ProductMapper());
        });
        _mapper = mappingConfig.CreateMapper();

        // _mockSet = new Mock<DbSet<User>>();

        _mockContext = new Mock<DesafioContext>(new DbContextOptions<DesafioContext>());

        //_mockService = new Mock<IUserService>(MockBehavior.Default);
        //_mockRepository = new Mock<IUserRepository>(MockBehavior.Default);
        //_mockContext.Setup(m => m.User).Returns(_mockSet.Object);
        //_mockController = new Mock<UserController>(_mockService.Object);

        _mockRepository = new Mock<IUserRepository>();
        _mockService = new UserService(_mockRepository.Object, _mapper);
        _mockController = new UserController(_mockService);
    }

    private NewUserRequest NewUser()
    {
        return new NewUserRequest()
        {
            Name = "joao Test",
            Cpf = "05741329705",
            Email = "joao@gmail.com",
            Phone = "21980099744"
        };
    }

    [Fact]
    public async Task Should_saveUser()
    {
        var userRequest = NewUser();

        // _mockSet.Setup(m => m.AddAsync(user, default)).Returns(Task.CompletedTask);
        // _mockSet.Setup(db => db.AddAsync(user,default)).Returns(Task.CompletedTask);
        // _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

        // _mockService.Setup(s => s.Save(userRequest));
        //  var controller = new UserController(_mockService.Object);

        var repository = new Mock<IUserRepository>();
        var mockService = new UserService(repository.Object, _mapper);
        var controller = new UserController(mockService);

        // Act
        var result = await controller.Post(userRequest);

        // Assert
        var actionResult = Assert.IsType<ActionResult<UserResponse>>(result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
    }

    [Fact]
    public async Task Get_ShouldReturnAllUser()
    {
        var userRequest = NewUser();
      

        var x =  await _mockService.Save(userRequest);
        await _mockService.Save(userRequest);
        await _mockService.Save(userRequest);

        // Act 
        var result = await _mockController.GetAll();

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<UserResponse>>>(result);
    }


    public async Task GetAuthor_ShouldReturnAuthor()
    {
        // Arrange
        var id = 1;
        var author = new User { Id = id, Name = "Test Author" };
        var data = new List<User> { author }.AsQueryable();

        _mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        _mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        _mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        _mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        _mockContext.Setup(m => m.User.FindAsync(id)).ReturnsAsync(author);

        // Act
        var result = await _mockController.Get(id);

        // Assert
        var actionResult = Assert.IsType<ActionResult<User>>(result);
        var okResult = Assert.IsType<User>(actionResult.Value);
        Assert.Equal(id, okResult.Id);
    }
}