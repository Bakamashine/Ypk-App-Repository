using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityFrameworkConfiguration;

public class StatusOrderConfiguration : IEntityTypeConfiguration<StatusOrder>
{
    public void Configure(EntityTypeBuilder<StatusOrder> builder)
    {
        builder.HasKey(role => role.Id);
        builder.HasIndex(role => role.Id).IsUnique();
        builder.Property(role => role.Id).HasMaxLength(250);
        builder.Property(role => role.StatusName).IsRequired();
    }
}