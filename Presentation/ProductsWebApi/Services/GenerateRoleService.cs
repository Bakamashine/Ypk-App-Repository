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
    public static class GenerateRoleService
    {
        public static async Task AddRoleInDataBase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
                await DbInitializer.Initialize(context, CancellationToken.None);

                foreach (var nameRole in Enum.GetNames<EnumRoles>())
                {
                    if (!context.Roles.Any(name => name.RoleName == nameRole))
                    {
                        await context.Roles.AddAsync(new Role()
                        {
                            Id = Guid.NewGuid(),
                            RoleName = nameRole,
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
