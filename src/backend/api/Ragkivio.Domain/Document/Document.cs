namespace Ragkivio.Domain.Document;

using Ragkivio.Domain.Common;

public class Document : Entity, IDisposable
{
    public string Title { get; set; } = string.Empty;
    public Stream Content { get; set; } = null!;
    public DocumentMetadata Metadata { get; set; } = null!;
    public DocumentStatus Status { get; set; } = DocumentStatus.None;

    public void Dispose()
    {
        Content?.Dispose();
    }
}