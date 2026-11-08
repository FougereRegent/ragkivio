namespace Ragkivio.Infrastructure.Common;

public static class InitName 
{
    public const string InitStorage = "InitFileStorage";
}

public interface IInit
{
    public Task InitAsync(CancellationToken cancellationToken);
}