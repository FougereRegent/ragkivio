namespace Ragkivio.Application.Common;

public abstract class InfrastructureException : Exception
{
    public abstract string ExceptionType { get; }

    public InfrastructureException(string message) : base(message)
    {

    }
}