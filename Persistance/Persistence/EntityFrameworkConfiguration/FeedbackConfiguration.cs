using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityFrameworkConfiguration;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(role => role.Id);
        builder.HasIndex(role => role.Id).IsUnique();
        builder.Property(role => role.Id).HasMaxLength(250);
        builder.Property(role => role.UserId).IsRequired();
        builder.Property(role => role.Raiting)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(role => role.Comment).IsRequired().HasMaxLength(1500);


        builder.HasOne(feedback => feedback.User)
            .WithMany(user => user.Feedbacks)
            .HasForeignKey(k => k.UserId);
    }
}