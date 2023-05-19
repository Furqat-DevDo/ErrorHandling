using System.Collections.Concurrent;
using System.Net;
using ErrorHandling.Exceptions;
using FluentValidation;

namespace ErrorHandling;

internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    private static readonly ConcurrentDictionary<Type, string> Codes = new ConcurrentDictionary<Type, string>();

    private const string ValidationFailedCode = "validation_failed";

    public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            DomainException ex => new ExceptionResponse(new { code = GetCode(ex), reason = ex.Message },
                HttpStatusCode.BadRequest),
            ValidationException ex => new ExceptionResponse(new
            {
                code = ValidationFailedCode,
                reason = ex.Errors.Select(error =>
                    new ValidationResponse(error.PropertyName, error.ErrorMessage))
            },
                HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(new { code = "error", reason = exception.Message },
                HttpStatusCode.InternalServerError)
        };

    private static string GetCode(Exception exception)
    {
        var type = exception.GetType();
        if (Codes.TryGetValue(type, out var code))
        {
            return code;
        }

        var exceptionCode = exception.GetExceptionCode();
        Codes.TryAdd(type, exceptionCode);

        return exceptionCode;
    }

    
}

public static class Extensions
{
    public static string GetExceptionCode(this Exception exception)
        => exception.GetType().Name.Underscore().Replace("_exception", string.Empty);

    public static string Underscore(this string value)
    => string.IsNullOrWhiteSpace(value)
        ? string.Empty
        : string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString()))
            .ToLowerInvariant();
}