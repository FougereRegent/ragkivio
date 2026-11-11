namespace Ragkivio.Graphql.Types.Document;

using HotChocolate.Resolvers;
using Ragkivio.Application.Document;
using Ragkivio.Application.Document.Dto;
using Ragkivio.Domain.Document;

public static class DocumentCreateMutation
{
    extension(IObjectTypeDescriptor<Mutation> descriptor)
    {
        public IObjectTypeDescriptor<Mutation> CreateDocument()
        {
            descriptor.Field("createDocument")
                .Argument("input", a => a.Type<CreateDocumentType>())
                .Resolve(HandleAsync)
                .Type<DocumentType>();
            return descriptor;
        }
    }

    private static async Task<Document> HandleAsync(IResolverContext ctx)
    {
        var createDocumentUseCase = ctx.Services.GetRequiredService<CreateDocumentUseCase>();
        var input = ctx.ArgumentValue<CreateDocumentInput>("input");
        var file = input.File;
        using var currentFileStream = new MemoryStream();

        await file.CopyToAsync(currentFileStream, ctx.RequestAborted);

        var createDocumentRequest = new CreateDocumentRequest(
                Title: file.Name,
                Content: currentFileStream,
                ProjectId: input.ProjectId
                );
        return new Document();
    }
}