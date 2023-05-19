using System.Runtime.Serialization;

namespace ErrorHandling.Exceptions;

[Serializable]
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }

    protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}