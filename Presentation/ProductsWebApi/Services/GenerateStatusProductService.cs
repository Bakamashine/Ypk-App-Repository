using Aplication;
using Domain.Model;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Services
{
    public static class GenerateStatusProductService
    {
        public static async Task AddStatusProductInDataBase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
                await DbInitializer.Initialize(context, CancellationToken.None);

                foreach (var nameStatus in Enum.GetNames<StatusProductEnum>())
                {
                    if (!context.StatusProducts.Any(name => name.StatusName == nameStatus))
                    {
                        await context.StatusProducts.AddAsync(new StatusProduct()
                        {
                            Id = Guid.NewGuid(),
                            StatusName = nameStatus,
                        });
                    }
                }
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
