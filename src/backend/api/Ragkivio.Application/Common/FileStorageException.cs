namespace Ragkivio.Application.Common;

public sealed class FileStorageException : InfrastructureException
{
    public override string ExceptionType => "file-storage";

    public FileStorageException(string message) : base(message) { }
}