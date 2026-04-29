using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<StatusOrder> StatusOrders { get; set; }
    DbSet<StatusProduct> StatusProducts { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Feedback> Feedbacks { get; set; }
    DbSet<Ypk> Ypks { get; set; }

    DbSet<RefreshToken> UserToken { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}