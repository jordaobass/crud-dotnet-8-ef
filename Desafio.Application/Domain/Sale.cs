using System.ComponentModel.DataAnnotations.Schema;
using Desafio.Application.exception;

namespace Desafio.Application.domain;

[Table("SALE")]
public class Sale
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<SaleProduct> SaleProducts { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public EnumStatusSale Status { get; set; }

    public Sale(User user)
    {
        User = user;
        Status = EnumStatusSale.AGUARDANDO_PAGAMENTO;
    }

    public Sale()
    {
        Status = EnumStatusSale.AGUARDANDO_PAGAMENTO;
    }

    public bool SaleCanBeChange()
    {
        return Status == EnumStatusSale.AGUARDANDO_PAGAMENTO;
    }

    public void AddItem(int productId)
    {
        SaleProducts.Add(new SaleProduct(this.Id, productId));
    }

    public void ChangeState(EnumStatusSale newStatus)
    {
        if (CanTransitionTo(newStatus))
        {
            Status = newStatus;
        }
        else
        {
            throw new  DomainException("Status Invalido");
        }
    }

    private bool CanTransitionTo(EnumStatusSale enumStatusSale)
    {
        switch (Status)
        {
            case EnumStatusSale.AGUARDANDO_PAGAMENTO:
                return enumStatusSale == EnumStatusSale.PAGAMENTO_APROVADO ||
                       enumStatusSale == EnumStatusSale.CANCELADA;
            case EnumStatusSale.PAGAMENTO_APROVADO:
                return enumStatusSale == EnumStatusSale.ENVIADO_TRANSPORTADORA ||
                       enumStatusSale == EnumStatusSale.CANCELADA;
            case EnumStatusSale.ENVIADO_TRANSPORTADORA:
                return enumStatusSale == EnumStatusSale.ENTREGUE;
            case EnumStatusSale.ENTREGUE:
                return false;
            case EnumStatusSale.CANCELADA:
                return false;
            default:
                throw new DomainException("Status Invalido");
        }
    }
}