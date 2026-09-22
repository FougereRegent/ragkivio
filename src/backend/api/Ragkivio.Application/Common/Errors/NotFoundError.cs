namespace Ragkivio.Application.Common.Errors;

public sealed class NotFoundError : BaseError {
    public NotFoundError(string ressource, string identifier, string message) : base(ressource, identifier, message) {}
}