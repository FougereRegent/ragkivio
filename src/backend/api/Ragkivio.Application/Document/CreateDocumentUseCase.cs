namespace Ragkivio.Application.Document;

using FluentResults;
using Ragkivio.Application.Common;
using Ragkivio.Application.Document.Dto;
using Ragkivio.Domain.Document;

public sealed class CreateDocumentUseCase : IUseCaseAsync<Document, CreateDocumentRequest>
{
    public Task<Result<Document>> HandleAsync(CreateDocumentRequest input, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}