using FluentResults;

namespace Ragkivio.Application.Common.Errors;

public abstract class BaseError : Error
{
    public string Identifier { get; protected set; }
    public string Ressource { get; protected set; }

    public BaseError(string ressource, string identifier, string message) : base(message)
    {
        this.Identifier = identifier;
        this.Ressource = ressource;
    }
}