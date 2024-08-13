namespace Desafio.Application.PayLoad.Response;

public class UserResponse
{
    public int Id { get; set; }
    public string Cpf { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; }
}