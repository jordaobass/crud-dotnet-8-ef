namespace Desafio.Application.exception;

public class DomainException : Exception
{
    public DomainException(string message)
        : base(message) { }
}