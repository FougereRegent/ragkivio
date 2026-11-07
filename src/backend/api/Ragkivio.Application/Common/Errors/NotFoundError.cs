namespace Ragkivio.Application.Common.Errors;

public sealed class NotFoundError : BaseError
{
    public NotFoundError(string resource, string identifier)
        : base(resource, identifier, $"{resource} with identifier {identifier} was not found") { }
}
