using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Feedbacks.UpdateFeedback;

public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand>
{
    private readonly IProductsDbContext context;

    public UpdateFeedbackCommandHandler(IProductsDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Feedbacks
                         .Include(u => u.User)
                         .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                     ?? throw new NotFoundException(nameof(Feedback), request.Id);

        var user = await context.Users.Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

        if (entity.UserId == request.CurrentUserId || user.Role.RoleName.Contains(nameof(EnumRoles.Admin)))
        {
            if (!string.IsNullOrEmpty(request.Comment))
                entity.Comment = request.Comment;
            if (request.Raiting != entity.Raiting)
                entity.Raiting = request.Raiting;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        throw new AccessException();
    }
}