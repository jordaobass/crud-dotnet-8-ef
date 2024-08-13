using Desafio.Application.PayLoad.Request;
using Desafio.Application.PayLoad.Response;
using Desafio.Application.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _service;

    public SaleController(ISaleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<SaleResponse>>> GetAll()
    {
        var response = await _service.GetAll();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<SaleResponse>>> Get(int id)
    {
        var response = await _service.Get(id);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<List<SaleResponse>>> Post(NewSaleRequest request)
    {
        var response = await _service.Save(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<List<SaleResponse>>> Put(UpdateSaleRequest request)
    {
        var response = await _service.AddItem(request);

        return Ok(response);
    }

    [HttpPatch("{idSale}/status/{idStatus}")]
    public async Task<ActionResult<List<SaleResponse>>> PutChangeStatus(int idSale, int idStatus)
    {
        var response = await _service.ChangeStatus(idSale, idStatus );

        return Ok(response);
    }

    [HttpDelete("{saleId}/product/{productId}")]
    public async Task<ActionResult<List<SaleResponse>>> DeleteItem(int saleId, int productId)
    {
        var response = await _service.RemoveItem(saleId, productId);

        return Ok(response);
    }
}