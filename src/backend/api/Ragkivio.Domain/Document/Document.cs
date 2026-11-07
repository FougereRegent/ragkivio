namespace Ragkivio.Domain.Document;

using Ragkivio.Domain.Common;

public class Document : Entity
{
    public string Title { get; set; } = string.Empty;
    public DocumentStatus Status { get; set; } = DocumentStatus.None;
}