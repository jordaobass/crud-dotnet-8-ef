using Desafio.Application.domain;

namespace Desafio.Application.PayLoad.Response;

public class SaleResponse
{
    public int Id { get; set; }
    public EnumStatusSale Status { get; set; }
    public List<ProductResponse> Product { get; set; }
    public UserResponse User { get; set; }
    public DateTime CreatedAt { get; set; }
}