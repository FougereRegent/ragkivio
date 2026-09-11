using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ragkivio.Persistence.Entities;

namespace Ragkivio.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(pre => pre.Id)
            .HasName("id");

        builder.Property(pre => pre.Email)
            .HasColumnName("email")
            .IsRequired(true);
        builder.Property(pre => pre.FirstName)
            .HasColumnName("first_name")
            .IsRequired(true);
        builder.Property(pre => pre.LastName)
            .HasColumnName("last_name")
            .IsRequired(true);
        builder.Property(pre => pre.AuthId)
            .HasColumnName("auth_id")
            .IsRequired(false)
            .HasDefaultValue(null);
        builder.Property(pre => pre.PhoneNumber)
            .HasColumnName("phone_number")
            .IsRequired(false);
        builder.Property(pre => pre.Config)
            .IsRequired(false)
            .HasColumnType("jsonb")
            .HasColumnName("config");

        builder.HasIndex(pre => pre.Email)
            .IsUnique();
    }
}