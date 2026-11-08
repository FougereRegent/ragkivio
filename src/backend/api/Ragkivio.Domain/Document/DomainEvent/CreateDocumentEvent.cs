using Ragkivio.Domain.Common;

namespace Ragkivio.Domain.Document.DomainEvent;

public sealed class CreateDocumentEvent : IDomainEvent
{

    public string Title { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public Stream Content { get; set; } = null!;

    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}