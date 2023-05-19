using ErrorHandling.Exceptions;

namespace ErrorHandling;

internal interface IExceptionToResponseMapper
{
    ExceptionResponse Map(Exception exception);
}