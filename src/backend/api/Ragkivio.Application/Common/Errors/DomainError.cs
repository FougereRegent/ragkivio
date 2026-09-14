namespace Ragkivio.Application.Common.Errors;

public sealed class DomainError : BaseError {
    public DomainError(string ressource, string message) : base(ressource, string.Empty, message){

    }
}