using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityFrameworkConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(role => role.Id);
        builder.HasIndex(role => role.Id).IsUnique();
        builder.Property(role => role.Id).HasMaxLength(250);
        builder.Property(role => role.RoleName).IsRequired().HasMaxLength(50);
    }
}