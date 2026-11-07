using Ragkivio.Domain.Common.Exceptions;

namespace Ragkivio.Application.Common.Errors;

public sealed class DomainError : BaseError
{
    public string PropertyName { get; }

    public DomainError(string resource, string message) : base(resource, string.Empty, message)
    {
        PropertyName = string.Empty;
    }

    public DomainError(string resource, BusinessException ex) : base(resource, string.Empty, ex.Message)
    {
        PropertyName = ex.PropertyName;
    }
}
