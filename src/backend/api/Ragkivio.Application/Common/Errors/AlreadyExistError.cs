namespace Ragkivio.Application.Common.Errors;

public sealed class AlreadyExistError : BaseError
{
    private const string defaultMessage = "Ressource {0} with identifier {1} already exist";

    public AlreadyExistError(string ressource, string identifier) : base(ressource, identifier, string.Format(defaultMessage, ressource, identifier))
    {

    }

}