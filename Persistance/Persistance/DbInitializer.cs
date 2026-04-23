namespace Persistance;

public class DbInitializer
{
    public static async Task Initialize(ProductsDbContext context, CancellationToken cancellation)
    {
        await context.Database.EnsureCreatedAsync(cancellation);
    }
}