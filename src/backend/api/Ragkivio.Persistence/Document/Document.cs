
namespace Ragkivio.Persistence.Document;

using Ragkivio.Persistence.Common.Entities;

internal enum DocumentStatus
{
    None,
    InTreatment,
    Indexed,
    Error,
}

internal sealed class Document : EntityWithTenancy
{
    internal string Title { get; set; } = string.Empty;
    internal string FileName { get; set; } = string.Empty;
    internal string FileExtension { get; set; } = string.Empty;
    internal string CheckSum { get; set; } = string.Empty;
    internal string MimeType { get; set; } = string.Empty;
    internal ulong Size { get; set; }
    internal DocumentStatus Status { get; set; }
}