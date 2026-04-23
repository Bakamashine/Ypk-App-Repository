using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<ProductsDbContext>(options => { options.UseNpgsql(connectionString); });
        services.AddScoped<IProductsDbContext, ProductsDbContext>(provider =>
            provider.GetRequiredService<ProductsDbContext>());
        return services;
    }
}