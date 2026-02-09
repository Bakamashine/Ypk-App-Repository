using Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.EntityFrameworkConfiguration
{
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
}
