using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityFrameworkConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(role => role.Id);
        builder.HasIndex(role => role.Id).IsUnique();
        builder.Property(role => role.Id).HasMaxLength(250);
        builder.Property(user => user.ProductId).IsRequired();
        builder.Property(user => user.StatusOrderId).IsRequired();
        builder.Property(user => user.CustomerId).IsRequired();
        builder.Property(user => user.ExecutorId).IsRequired(false);
        builder.Property(user => user.Date).IsRequired();
        builder.Property(user => user.CustomersComment).IsRequired(false).HasMaxLength(1500);
        builder.Property(user => user.UserComment).IsRequired(false).HasMaxLength(1500);

        builder.HasOne(order => order.User)
            .WithMany(user => user.Orders)
            .HasForeignKey(k => k.CustomerId);

        builder.HasOne(order => order.Product)
            .WithMany(product => product.Orders)
            .HasForeignKey(k => k.ProductId);

        builder.HasOne(order => order.StatusOrder)
            .WithMany(statusOrder => statusOrder.Orders)
            .HasForeignKey(k => k.StatusOrderId);
    }
}