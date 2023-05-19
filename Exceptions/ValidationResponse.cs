namespace ErrorHandling.Exceptions;

public class ValidationResponse
{
    public string Field { get; }
    public string Error { get; }

    public ValidationResponse(string field, string error)
    {
        Field = field;
        Error = error;
    }
}