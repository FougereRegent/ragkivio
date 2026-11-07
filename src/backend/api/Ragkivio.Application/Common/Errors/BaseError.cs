using FluentResults;

namespace Ragkivio.Application.Common.Errors;

public abstract class BaseError : Error
{
    public string Identifier { get; protected set; }
    public string Resource { get; protected set; }

    public BaseError(string resource, string identifier, string message) : base(message)
    {
        this.Identifier = identifier;
        this.Resource = resource;
    }
}
