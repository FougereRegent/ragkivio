namespace Ragkivio.Application.Common.Errors;

public class InfrastructureError : BaseError
{
    public string ErrorType { get; init; } = string.Empty;

    public InfrastructureError(string ressource, InfrastructureException ex) : base(ressource, string.Empty, ex.Message)
    {
        this.ErrorType = ex.ExceptionType;
    }
}