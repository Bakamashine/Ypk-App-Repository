using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityFrameworkConfiguration;

public class StatusProductConfiguration : IEntityTypeConfiguration<StatusProduct>
{
    public void Configure(EntityTypeBuilder<StatusProduct> builder)
    {
        builder.HasKey(role => role.Id);
        builder.HasIndex(role => role.Id).IsUnique();
        builder.Property(role => role.Id).HasMaxLength(250);
        builder.Property(role => role.StatusName).IsRequired();
    }
}