namespace Ragkivio.Application.Document.Services;

public readonly record struct FileReferenceKey(
        string Key
);

public readonly record struct FileInputReferenceKey(
        Stream Content,
        string Key,
        string MimeType
);

public interface IFileStorageService
{
    public Task SaveFileAsync(FileInputReferenceKey referenceKey, CancellationToken token);

    public Task<Stream> GetFileAsync(FileReferenceKey referenceKey, CancellationToken token);
}
