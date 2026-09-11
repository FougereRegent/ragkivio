namespace Ragkivio.Domain.Document;

public class Document : Common.Entity
{
    public string Name { get; set; } = string.Empty;
    public long Size { get; set; }
    public DocumentStatus Status { get; set; } = DocumentStatus.None;

    public DocumentContent? DocumentContent { get; set; } = null;

}