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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(role => role.Id);
            builder.HasIndex(role => role.Id).IsUnique();
            builder.Property(role => role.Id).HasMaxLength(250);
            builder.Property(user => user.ProductId).IsRequired();
            builder.Property(user => user.StatusOrderId).IsRequired();
            builder.Property(user => user.UserId).IsRequired();
            builder.Property(user => user.Date).IsRequired();
            builder.Property(user => user.CustomersComment).IsRequired(false).HasMaxLength(1500);
            builder.Property(user => user.UserComment).IsRequired(false).HasMaxLength(1500);

            builder.HasOne(order => order.User)
               .WithMany(user => user.Orders)
               .HasForeignKey(k => k.UserId);

            builder.HasOne(order => order.Product)
               .WithMany(product => product.Orders)
               .HasForeignKey(k => k.ProductId);

            builder.HasOne(order => order.StatusOrder)
               .WithMany(statusOrder => statusOrder.Orders)
               .HasForeignKey(k => k.StatusOrderId);
        }
    }
}

