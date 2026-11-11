using Ragkivio.Domain.Common;

namespace Ragkivio.Domain.Document;

public interface IDocumentRepository : IGenericRepository<Document>
{
    Task<Document?> GetByTitleAsync(string title, CancellationToken token);
}
