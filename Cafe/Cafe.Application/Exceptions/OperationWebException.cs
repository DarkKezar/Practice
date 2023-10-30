using System.Net;

namespace Cafe.Application.Exceptions;

public class OperationWebException : System.Exception
{
    public HttpStatusCode HttpStatusCode { get; }

    public OperationWebException() { }

    public OperationWebException(string message) : base(message) { }

    public OperationWebException(string message, HttpStatusCode httpStatusCode) : base(message) 
    { 
        HttpStatusCode = httpStatusCode;
    }

    public OperationWebException(string message, System.Exception inner) : base(message, inner) { }

    protected OperationWebException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
