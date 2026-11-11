namespace Ragkivio.Persistence.Document;

using Ragkivio.Domain.Document;
using Riok.Mapperly.Abstractions;
using DocumentDomain = Ragkivio.Domain.Document.Document;
using DocumentPersistence = Ragkivio.Persistence.Document.Document;
using DocumentStatusDomain = Ragkivio.Domain.Document.DocumentStatus;
using DocumentStatusPersistence = Ragkivio.Persistence.Document.DocumentStatus;

[Mapper]
internal static partial class DocumentMapper
{
    [MapperIgnoreSource(nameof(DocumentDomain.Content))]
    [MapperIgnoreTarget(nameof(DocumentPersistence.TenantId))]
    [MapperIgnoreTarget(nameof(DocumentPersistence.IsDelete))]
    [MapperIgnoreTarget(nameof(DocumentPersistence.DeletedAt))]
    [MapProperty(nameof(DocumentDomain.Status), nameof(DocumentPersistence.Status), Use = nameof(MapStatusToPersistence))]
    [MapPropertyFromSource(nameof(DocumentPersistence.FileName), Use = nameof(MapFileName))]
    [MapPropertyFromSource(nameof(DocumentPersistence.FileExtension), Use = nameof(MapFileExtension))]
    [MapPropertyFromSource(nameof(DocumentPersistence.CheckSum), Use = nameof(MapCheckSum))]
    [MapPropertyFromSource(nameof(DocumentPersistence.MimeType), Use = nameof(MapMimeType))]
    [MapPropertyFromSource(nameof(DocumentPersistence.Size), Use = nameof(MapSize))]
    public static partial DocumentPersistence ToPersistence(DocumentDomain document);

    [MapperIgnoreSource(nameof(DocumentPersistence.TenantId))]
    [MapperIgnoreSource(nameof(DocumentPersistence.IsDelete))]
    [MapperIgnoreSource(nameof(DocumentPersistence.DeletedAt))]
    [MapperIgnoreTarget(nameof(DocumentDomain.Content))]
    [MapProperty(nameof(DocumentPersistence.Status), nameof(DocumentDomain.Status), Use = nameof(MapStatusToDomain))]
    [MapPropertyFromSource(nameof(DocumentDomain.Metadata), Use = nameof(MapMetadata))]
    public static partial DocumentDomain ToDomain(DocumentPersistence document);

    public static partial IQueryable<DocumentDomain> DocumentToDomain(this IQueryable<DocumentPersistence> documents);

    [MapperIgnoreSource(nameof(DocumentDomain.Content))]
    [MapperIgnoreTarget(nameof(DocumentPersistence.TenantId))]
    [MapperIgnoreTarget(nameof(DocumentPersistence.IsDelete))]
    [MapperIgnoreTarget(nameof(DocumentPersistence.DeletedAt))]
    [MapProperty(nameof(DocumentDomain.Status), nameof(DocumentPersistence.Status), Use = nameof(MapStatusToPersistence))]
    [MapPropertyFromSource(nameof(DocumentPersistence.FileName), Use = nameof(MapFileName))]
    [MapPropertyFromSource(nameof(DocumentPersistence.FileExtension), Use = nameof(MapFileExtension))]
    [MapPropertyFromSource(nameof(DocumentPersistence.CheckSum), Use = nameof(MapCheckSum))]
    [MapPropertyFromSource(nameof(DocumentPersistence.MimeType), Use = nameof(MapMimeType))]
    [MapPropertyFromSource(nameof(DocumentPersistence.Size), Use = nameof(MapSize))]
    public static partial void Hydrate(DocumentDomain source, DocumentPersistence target);

    private static string MapFileName(DocumentDomain document) => document.Metadata.FileName;
    private static string MapFileExtension(DocumentDomain document) => document.Metadata.FileExtension;
    private static string MapCheckSum(DocumentDomain document) => document.Metadata.CheckSum;
    private static string MapMimeType(DocumentDomain document) => document.Metadata.MimeType;
    private static ulong MapSize(DocumentDomain document) => (ulong)document.Metadata.Size;

    private static DocumentMetadata MapMetadata(DocumentPersistence document) => new()
    {
        FileName = document.FileName,
        FileExtension = document.FileExtension,
        CheckSum = document.CheckSum,
        MimeType = document.MimeType,
        Size = (long)document.Size,
    };

    private static DocumentStatusPersistence MapStatusToPersistence(DocumentStatusDomain status) => (DocumentStatusPersistence)(int)status;
    private static DocumentStatusDomain MapStatusToDomain(DocumentStatusPersistence status) => (DocumentStatusDomain)(int)status;
}
