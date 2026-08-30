using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ragkivio.Persistence.Entities;

namespace Ragkivio.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.Property(pre => pre.Email)
            .HasColumnName("email")
            .HasDefaultValue("");
        builder.Property(pre => pre.FirstName)
            .HasColumnName("first_name")
            .HasDefaultValue("");
        builder.Property(pre => pre.LastName)
            .HasColumnName("last_name")
            .HasDefaultValue("");
    }
}