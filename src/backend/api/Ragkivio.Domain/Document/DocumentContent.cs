namespace Ragkivio.Domain.Document;

public class DocumentContent
{
    public Stream Content { get; set; } = null!;
    public string FileName { get; set; } = string.Empty;
}