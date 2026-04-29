using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        var env = configuration["ASPNETCORE_ENVIRONMENT"];

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (connectionString == null) return;
            if (env == "Development")
                options.UseSqlite(connectionString, b => { b.MigrationsAssembly("Persistence"); });
            else
                options.UseNpgsql(connectionString, b => { b.MigrationsAssembly("Persistence"); });
        });

        // services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());
        return services;
    }
}