using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Ypks.DeleteYpk;

public class DeleteYpkCommandHandler : IRequestHandler<DeleteYpkCommand>
{
    private readonly IProductsDbContext context;

    public DeleteYpkCommandHandler(IProductsDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteYpkCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Ypks
                         .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                     ?? throw new NotFoundException(nameof(Ypk), request.Id);

        entity.IsActive = false;
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}