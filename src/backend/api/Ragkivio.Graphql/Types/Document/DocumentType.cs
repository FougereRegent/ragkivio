namespace Ragkivio.Graphql.Types.Document;

using Ragkivio.Domain.Document;

public sealed class CreateDocumentInput
{
    public IFile File { get; set; } = null!;
    public Guid ProjectId { get; set; }
}

public sealed class CreateDocumentType : InputObjectType<CreateDocumentInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<CreateDocumentInput> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(f => f.File)
            .Type<UploadType>();

        descriptor.Field(f => f.ProjectId)
            .Type<UuidType>();
    }
}

public sealed class DocumentType : ObjectType<Document>
{
    protected override void Configure(IObjectTypeDescriptor<Document> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(f => f.Id)
            .Type<UuidType>();

        descriptor.Field(f => f.Title)
            .Type<StringType>();

        descriptor.Field(f => f.Status)
            .Type<EnumType<DocumentStatus>>();

        descriptor.Ignore(f => f.Content);
    }
}