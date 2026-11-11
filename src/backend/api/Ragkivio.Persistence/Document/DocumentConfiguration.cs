using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ragkivio.Persistence.Document;

internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("documents");
        builder.HasKey(pre => pre.Id)
            .HasName("pk_document");

        builder.Property(pre => pre.Title)
            .HasColumnName("title")
            .IsRequired(true);

        builder.Property(pre => pre.FileName)
            .HasColumnName("file_name")
            .IsRequired(true);

        builder.Property(pre => pre.FileExtension)
            .HasColumnName("file_extension")
            .IsRequired(true);

        builder.Property(pre => pre.MimeType)
            .HasColumnName("mime_type")
            .IsRequired(true);

        builder.Property(pre => pre.CheckSum)
            .HasColumnName("checksum")
            .IsRequired(true);

        builder.Property(pre => pre.Size)
            .HasColumnName("size")
            .IsRequired(true);

        builder.Property(pre => pre.Status)
            .HasColumnName("status")
            .HasDefaultValue(DocumentStatus.None);

        builder.HasIndex(pre => new { pre.TenantId, pre.Title })
            .IsUnique();

        builder.HasIndex(pre => new { pre.CheckSum, pre.TenantId });
    }
}