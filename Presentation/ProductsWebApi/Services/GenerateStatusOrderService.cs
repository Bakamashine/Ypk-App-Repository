using Application;
using Domain.Model;
using Persistence;

namespace ProductsWebApi.Services;

public static class GenerateStatusOrderService
{
    public static async Task AddStatusOrderInDataBase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        try
        {
            var context = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
            await DbInitializer.Initialize(context, CancellationToken.None);

            foreach (var nameStatus in Enum.GetNames<StatusOrderEnum>())
                if (!context.StatusOrders.Any(name => name.StatusName == nameStatus))
                    await context.StatusOrders.AddAsync(new StatusOrder
                    {
                        Id = Guid.NewGuid(),
                        StatusName = nameStatus
                    });

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}