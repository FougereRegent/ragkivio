namespace Ragkivio.Application.Common.Errors;

public sealed class AlreadyExistError : BaseError
{
    private const string DefaultMessage = "{0} with identifier {1} already exists";

    public AlreadyExistError(string resource, string identifier)
        : base(resource, identifier, string.Format(DefaultMessage, resource, identifier)) { }
}
