namespace Ragkivio.Persistence.Configuration.Document;

using Microsoft.Extensions.DependencyInjection;
using Ragkivio.Domain.Document;
using Ragkivio.Persistence.Document;

public static class DocumentConfiguration
{
    extension(IServiceCollection serviceCollection)
    {
        public IServiceCollection AddDocumentPersistence()
        {
            serviceCollection.AddScoped<IDocumentRepository, DocumentRepository>();
            return serviceCollection;
        }
    }
}
