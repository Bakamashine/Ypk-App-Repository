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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Id).IsUnique();
            builder.Property(user => user.Id).HasMaxLength(250);
            builder.Property(user => user.Fullname).HasMaxLength(150).IsRequired();
            builder.Property(user => user.HashPassword).HasMaxLength(250).IsRequired();
            builder.Property(user => user.PhoneNumber).HasMaxLength(12).IsRequired();
            builder.Property(user => user.RoleId).IsRequired();
            builder.Property(user => user.IsActive).IsRequired();
            builder.Property(user => user.UserInfo).IsRequired(false);

            builder.HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(k => k.RoleId);

        }
    }
}
