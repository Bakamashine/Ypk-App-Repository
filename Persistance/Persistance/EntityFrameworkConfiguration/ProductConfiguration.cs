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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(role => role.Id);
            builder.HasIndex(role => role.Id).IsUnique();
            builder.Property(role => role.Id).HasMaxLength(250);
            builder.Property(role => role.ProductName).IsRequired();
            builder.Property(user => user.StatusProductId).IsRequired();
            builder.Property(user => user.YpkId).IsRequired();
            builder.Property(user => user.UserId).IsRequired();
            builder.Property(user => user.Raiting).IsRequired().HasAnnotation("Range", new { Minimum = 1, Maximum = 5 }); 
            builder.Property(user => user.ProductCost).IsRequired().HasPrecision(9,2);
            builder.Property(user => user.IsProduct).IsRequired();
            builder.Property(user => user.Adress).IsRequired();
            builder.Property(user => user.Photo).IsRequired(false);

            builder.HasOne(product => product.User)
               .WithMany(user => user.Products)
               .HasForeignKey(k => k.UserId);

            builder.HasOne(product => product.Ypk)
               .WithMany(ypk => ypk.Products)
               .HasForeignKey(k => k.YpkId);

            builder.HasOne(product => product.StatusProduct)
               .WithMany(statusProduct => statusProduct.Products)
               .HasForeignKey(k => k.StatusProductId);
        }
    }
}
