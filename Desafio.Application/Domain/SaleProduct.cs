namespace Desafio.Application.domain;

public class SaleProduct
{
    public int SaleId { get; set; }
    public Sale Sale { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public SaleProduct()
    {
    }

    public SaleProduct(int saleId, int productId)
    {
        SaleId = saleId;
        ProductId = productId;
    }
}