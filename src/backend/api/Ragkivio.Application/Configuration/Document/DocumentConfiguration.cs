using Microsoft.Extensions.DependencyInjection;
using Ragkivio.Application.Document;
using Ragkivio.Application.Document.Handlers;
using Ragkivio.Domain.Common;
using Ragkivio.Domain.Document.DomainEvent;

namespace Ragkivio.Application.Configuration.Application;

public static class DocumentConfiguration
{
    extension(IServiceCollection serviceCollection)
    {
        public IServiceCollection AddDocument()
        {
            serviceCollection
                .AddTransient<CreateDocumentUseCase>();
            return serviceCollection;
        }
    }
}