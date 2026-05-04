using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context, CancellationToken cancellation)
    {
        await context.Database.MigrateAsync(cancellation);
    }
}