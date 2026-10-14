namespace Ragkivio.Domain.Common.Exceptions;

public sealed class LengthTooBigException(string propertyName, int maxLength) : BusinessException(propertyName, $"this field is bigger than {maxLength}")
{

}