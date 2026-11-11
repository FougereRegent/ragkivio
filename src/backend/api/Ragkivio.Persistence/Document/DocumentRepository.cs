namespace Ragkivio.Persistence.Document;

using DocumentPersistence = Ragkivio.Persistence.Document.Document;
using DocumentDomain = Ragkivio.Domain.Document.Document;
using Ragkivio.Application.Configuration;
using Ragkivio.Domain.Document;
using Ragkivio.Persistence.Common;
using Microsoft.EntityFrameworkCore;

internal sealed class DocumentRepository : GenericRepository<DocumentPersistence, DocumentDomain>, IDocumentRepository
{
    public DocumentRepository(RagkivioContext context, IDomainEventCollector domainEventCollector)
        : base(context, domainEventCollector)
    {
    }

    private protected override void HydratePersistenceEntity(DocumentDomain domainEntity, DocumentPersistence persistenceEntity)
    {
        DocumentMapper.Hydrate(domainEntity, persistenceEntity);
    }

    private protected override IQueryable<DocumentDomain> ProjectionToDomain(IQueryable<DocumentPersistence> collection)
    {
        return collection.DocumentToDomain();
    }

    private protected override DocumentDomain ToDomain(DocumentPersistence entity)
    {
        return DocumentMapper.ToDomain(entity);
    }

    private protected override DocumentPersistence ToPersistence(DocumentDomain entity)
    {
        return DocumentMapper.ToPersistence(entity);
    }

    public async Task<DocumentDomain?> GetByTitleAsync(string title, CancellationToken token) =>
        await this.GetAll().FirstOrDefaultAsync(pre => pre.Title == title, token);
}
