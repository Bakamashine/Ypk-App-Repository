using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DbInitializer
{
    public static async Task Initialize(ProductsDbContext context, CancellationToken cancellation)
    {
        await context.Database.MigrateAsync(cancellation);
    }
}