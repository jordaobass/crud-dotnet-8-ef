
namespace Desafio.Application.PayLoad.Request;

public class NewSaleRequest
{
    public List<int> ListProductId { get; set; }
    public int UserId { get; set; }
}