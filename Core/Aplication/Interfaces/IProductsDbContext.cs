using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Aplication.Interfaces
{
    public interface IProductsDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<StatusOrder> StatusOrders { get; set; }
        DbSet<StatusProduct> StatusProducts { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Feedback> Feedbacks { get; set; }
        DbSet<Ypk> Ypks { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
