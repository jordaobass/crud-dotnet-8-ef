using Desafio.Application.PayLoad.Request;
using Desafio.Application.PayLoad.Response;
using Desafio.Application.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    
    public ProductController(IProductService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ProductResponse>>> GetAll()
    {
        var response = await _service.GetAll();

        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<List<ProductResponse>>> Get(int id)
    {
        var response = await _service.GetAll();

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<List<UserResponse>>> Post(NewProductRequest request)
    {
        var response = await _service.Save(request);

        return Ok(response);
    }
}