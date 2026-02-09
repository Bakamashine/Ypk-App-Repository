using Aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<ProductsDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IProductsDbContext, ProductsDbContext>(provider =>
                provider.GetRequiredService<ProductsDbContext>());
            return services;

        }
    }
}
