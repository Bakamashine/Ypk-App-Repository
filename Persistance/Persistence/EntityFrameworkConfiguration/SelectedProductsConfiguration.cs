using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityFrameworkConfiguration
{
    public class SelectedProductsConfiguration : IEntityTypeConfiguration<SelectedProducts>
    {
        public void Configure(EntityTypeBuilder<SelectedProducts> builder)
        {
            builder.HasKey(selectedProducts => selectedProducts.Id);
            builder.HasIndex(selectedProducts => selectedProducts.Id).IsUnique();
            builder.Property(selectedProducts => selectedProducts.Id).HasMaxLength(250);
            builder.Property(selectedProducts => selectedProducts.ProductId).IsRequired();
            builder.Property(selectedProducts => selectedProducts.UserId).IsRequired();

            builder.HasOne(sp => sp.User)
                .WithMany(u => u.SelectedProducts)
                .HasForeignKey(sp => sp.UserId);

            builder.HasOne(sp => sp.Product)
                .WithMany(u => u.SelectedProducts)
                .HasForeignKey(sp => sp.ProductId);
        }
    }
}
