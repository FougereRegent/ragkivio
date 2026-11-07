using Ragkivio.Domain.Common.Exceptions;

namespace Ragkivio.Application.Common.Errors;

public sealed class DomainError : BaseError {
    public DomainError(string ressource, string message) : base(ressource, string.Empty, message){

    }

    public DomainError(string ressource, BusinessException ex) : base(ressource, string.Empty, ex.Message) {}
}