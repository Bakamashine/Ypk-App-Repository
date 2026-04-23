using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Users.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IProductsDbContext context;

    public DeleteUserCommandHandler(IProductsDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Users
                         .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                     ?? throw new NotFoundException(nameof(User), request.Id);

        entity.IsActive = false;
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}