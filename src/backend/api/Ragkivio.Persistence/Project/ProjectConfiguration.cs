using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ragkivio.Persistence.Project;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");
        builder.HasKey(pre => pre.Id)
            .HasName("pk_projects");

        builder.Property(pre => pre.Name)
            .HasColumnName("name")
            .HasMaxLength(Project.NameSizeMax)
            .IsRequired(true);

        builder.Property(pre => pre.Description)
            .HasColumnName("description")
            .HasMaxLength(Project.DescriptionMaxSize);

        builder.Property(pre => pre.Color)
            .HasColumnName("color");

        builder.Property(pre => pre.IsShared)
            .HasColumnName("is_shared")
            .HasDefaultValue(false);

        builder.HasIndex(pre => new { pre.Name, pre.TenantId })
            .IsUnique();
    }
}