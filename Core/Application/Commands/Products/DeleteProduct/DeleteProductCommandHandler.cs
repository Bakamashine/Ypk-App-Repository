using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Products.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IApplicationDbContext context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Products
                         .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                     ?? throw new NotFoundException(nameof(Product), request.Id);

        entity.StatusProduct =
            context.StatusProducts.FirstOrDefault(st => st.StatusName.Contains(nameof(StatusProductEnum.Deleted)));
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}