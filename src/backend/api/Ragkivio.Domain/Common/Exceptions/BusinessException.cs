namespace Ragkivio.Domain.Common.Exceptions;

public class BusinessException : Exception {
    public string PropertyName {get; init; }

    public BusinessException(string propertyName, string message) : base(message) {
        this.PropertyName = propertyName; 
    }
}