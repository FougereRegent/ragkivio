namespace Ragkivio.Application.Document.Dto;

public record CreateDocumentRequest(
        string Title,
        Guid ProjectId,
        Stream Content
        );