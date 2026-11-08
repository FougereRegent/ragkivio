namespace Ragkivio.Application.Document.Services;

public readonly record struct FileReferenceKey(
        string Key
);

public readonly record struct FileInputReferenceKey(
        Stream Content,
        string Key,
        string MimeType
);

public interface IFileStorage
{

}