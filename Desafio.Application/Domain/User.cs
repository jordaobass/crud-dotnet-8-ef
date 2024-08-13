using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio.Application.domain;

[Table("USER")]
public class User
{
    public int  Id { get; set; }
    public string Cpf { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
}