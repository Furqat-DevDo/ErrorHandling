namespace ErrorHandling.Exceptions;

public class AppException : Exception
{
    public int StatusCode { get; } = 500;

    public AppException() : base() { }

    public AppException(string message) : base(message) { }

    public AppException(int code, string message) : base(message)
    {
        StatusCode = code;
    }
}