using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityFrameworkConfiguration;

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
        builder.Property(user => user.YpkId).IsRequired(false);

        builder.HasOne(user => user.Role)
            .WithMany(role => role.Users)
            .HasForeignKey(k => k.RoleId);


        builder.HasOne(user => user.Ypk)
            .WithMany(ypk => ypk.Users)
            .HasForeignKey(user => user.YpkId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}