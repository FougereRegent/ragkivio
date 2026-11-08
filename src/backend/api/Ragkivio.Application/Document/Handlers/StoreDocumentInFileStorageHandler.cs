using Ragkivio.Application.Document.Services;
using Ragkivio.Domain.Common;
using Ragkivio.Domain.Document.DomainEvent;

namespace Ragkivio.Application.Document.Handlers;

internal sealed class StoreDocumentInFileStorageHandler(IFileStorageService fileStorageService) : IDomainEventHandler<CreateDocumentEvent>
{
    public async Task HandleAsync(CreateDocumentEvent @event, CancellationToken cancellationToken = default)
    {
        var fileInput = new FileInputReferenceKey(
                Content: @event.Content,
                Key: @event.Key,
                MimeType: @event.MimeType
                );

        await fileStorageService.SaveFileAsync(fileInput, cancellationToken);
    }
}