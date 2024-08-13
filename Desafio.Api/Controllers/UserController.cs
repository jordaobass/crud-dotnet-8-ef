using Desafio.Application.PayLoad.Request;
using Desafio.Application.PayLoad.Response;
using Desafio.Application.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAll()
    {
        var response = await _service.GetAll();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> Get(int id)
    {
        var response = await _service.Get(id);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<UserResponse>> Post(NewUserRequest request)
    {
       var response = await _service.Save(request);
       //return Ok(response);
       //return Created(new Uri(Request.GetEncodedUrl()+ "/" + response.Id), response);
       return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
       // return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
    }
}