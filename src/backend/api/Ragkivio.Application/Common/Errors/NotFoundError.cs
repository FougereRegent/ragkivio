namespace Ragkivio.Application.Common.Errors;

public sealed class NotFoundError : BaseError {
    public NotFoundError(string ressource, string identifier) : base(ressource, identifier, $"{ressource} with search identifier : {identifier} not found") {}
}