namespace Desafio.Application.PayLoad.Request;

public class NewUserRequest
{
    public string Cpf { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}