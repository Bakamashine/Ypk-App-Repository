using Application.Interfaces;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFrameworkConfiguration;

namespace Persistence;

public class ProductsDbContext : DbContext, IProductsDbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }


    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<StatusOrder> StatusOrders { get; set; }
    public DbSet<StatusProduct> StatusProducts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Ypk> Ypks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new YpkConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new StatusOrderConfiguration());
        builder.ApplyConfiguration(new StatusProductConfiguration());
        builder.ApplyConfiguration(new FeedbackConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
        base.OnModelCreating(builder);
    }
}