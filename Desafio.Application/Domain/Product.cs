using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio.Application.domain;

[Table("PRODUCT")]
public class Product
{
    public int Id { get; set; }
    public String Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}