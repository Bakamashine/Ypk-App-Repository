using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Ypks.UpdateYpk;

public class UpdateYpkCommandHandler : IRequestHandler<UpdateYpkCommand>
{
    private readonly IApplicationDbContext context;

    public UpdateYpkCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(UpdateYpkCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Ypks
                         .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                     ?? throw new NotFoundException(nameof(Ypk), request.Id);

        var user = await context.Users.Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

        if (user.Role.RoleName.Contains(nameof(EnumRoles.Admin)))
        {
            if (!string.IsNullOrEmpty(request.YpkName))
                entity.YpkName = request.YpkName;

            entity.Description = request.Description;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        throw new AccessException();
    }
}