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
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(role => role.Id);
            builder.HasIndex(role => role.Id).IsUnique();
            builder.Property(role => role.Id).HasMaxLength(250);
            builder.Property(role => role.UserId).IsRequired();
            builder.Property(role => role.Raiting).IsRequired().HasAnnotation("Range", new { Minimum = 1, Maximum = 5 }); ;
            builder.Property(role => role.Comment).IsRequired().HasMaxLength(1500);


            builder.HasOne(feedback => feedback.User)
               .WithMany(user => user.Feedbacks)
               .HasForeignKey(k => k.UserId);
        }
    }
}
