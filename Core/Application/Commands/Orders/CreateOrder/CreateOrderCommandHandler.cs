using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Orders.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IProductsDbContext context;

    public CreateOrderCommandHandler(IProductsDbContext context)
    {
        this.context = context;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newOrder = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CurrentUserId,
            ProductId = request.ProductId,
            StatusOrder =
                await context.StatusOrders.FirstOrDefaultAsync(x => x.StatusName == nameof(StatusOrderEnum.PlaceAn),
                    cancellationToken),
            Date = DateTime.UtcNow,
            CustomersComment = request.CustomersComment,
            UserComment = request.UserComment
        };

        await context.Orders.AddAsync(newOrder, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return newOrder.Id;
    }
}