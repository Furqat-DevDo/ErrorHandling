namespace ErrorHandling.Exceptions;

public class CustomException : DomainException
{
    public CustomException(string message) : base(message)
    {
    }
}