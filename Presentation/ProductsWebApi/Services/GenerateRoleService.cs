using Application;
using Domain.Model;
using Persistence;

namespace ProductsWebApi.Services;

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
                if (!context.Roles.Any(name => name.RoleName == nameRole))
                    await context.Roles.AddAsync(new Role
                    {
                        Id = Guid.NewGuid(),
                        RoleName = nameRole
                    });

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}