namespace Ragkivio.Domain.Common.Exceptions;

public sealed class FutureDateException : BusinessException
{
    public FutureDateException(string propertyName, string date) : base(propertyName, $"{date} cannot be in future, it should be inferior or equal at actual date")
    {

    }
}