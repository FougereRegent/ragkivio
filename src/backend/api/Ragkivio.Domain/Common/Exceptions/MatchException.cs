namespace Ragkivio.Domain.Common.Exceptions;

public sealed class MatchException : BusinessException {
    public MatchException(string propertyName, string regex) : base(propertyName, $"Should respect this pattern {regex}") {

    }
}