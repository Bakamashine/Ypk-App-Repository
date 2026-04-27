using Application;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace ProductsWebApi.Services;

public static class DatabaseInitializerService
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        try
        {
            var context = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();

            // Добавляем все данные в одном методе
            await InitializeRoles(context);
            await InitializeStatusOrders(context);
            await InitializeStatusProducts(context);

            Console.WriteLine("Инициализация базы данных завершена");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка инициализации: {ex}");
            throw;
        }
    }

    private static async Task InitializeRoles(ProductsDbContext context)
    {
        var existingRoles = await context.Roles
            .Select(r => r.RoleName)
            .ToListAsync();

        var rolesToAdd = Enum.GetNames<EnumRoles>()
            .Where(roleName => !existingRoles.Contains(roleName))
            .Select(roleName => new Role
            {
                Id = Guid.NewGuid(),
                RoleName = roleName
            })
            .ToList();

        if (rolesToAdd.Any())
        {
            await context.Roles.AddRangeAsync(rolesToAdd);
            await context.SaveChangesAsync();
            Console.WriteLine($"Добавлено {rolesToAdd.Count} ролей");
        }
    }

    private static async Task InitializeStatusOrders(ProductsDbContext context)
    {
        var existingStatuses = await context.StatusOrders
            .Select(s => s.StatusName)
            .ToListAsync();

        var statusesToAdd = Enum.GetNames<StatusOrderEnum>()
            .Where(statusName => !existingStatuses.Contains(statusName))
            .Select(statusName => new StatusOrder
            {
                Id = Guid.NewGuid(),
                StatusName = statusName
            })
            .ToList();

        if (statusesToAdd.Any())
        {
            await context.StatusOrders.AddRangeAsync(statusesToAdd);
            await context.SaveChangesAsync();
            Console.WriteLine($"Добавлено {statusesToAdd.Count} статусов заказов");
        }
    }

    private static async Task InitializeStatusProducts(ProductsDbContext context)
    {
        var existingStatuses = await context.StatusProducts
            .Select(s => s.StatusName)
            .ToListAsync();

        var statusesToAdd = Enum.GetNames<StatusProductEnum>()
            .Where(statusName => !existingStatuses.Contains(statusName))
            .Select(statusName => new StatusProduct
            {
                Id = Guid.NewGuid(),
                StatusName = statusName
            })
            .ToList();

        if (statusesToAdd.Any())
        {
            await context.StatusProducts.AddRangeAsync(statusesToAdd);
            await context.SaveChangesAsync();
            Console.WriteLine($"Добавлено {statusesToAdd.Count} статусов продуктов");
        }
    }
}