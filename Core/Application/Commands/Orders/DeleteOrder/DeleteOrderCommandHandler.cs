using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Orders.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IProductsDbContext context;

    public DeleteOrderCommandHandler(IProductsDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Orders
                         .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                     ?? throw new NotFoundException(nameof(Order), request.Id);

        entity.StatusOrder =
            await context.StatusOrders.FirstOrDefaultAsync(x => x.StatusName == nameof(StatusOrderEnum.Cancelled),
                cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}