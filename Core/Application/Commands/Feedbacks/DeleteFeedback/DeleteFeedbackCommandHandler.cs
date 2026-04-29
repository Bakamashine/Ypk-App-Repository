using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Feedbacks.DeleteFeedback;

public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand>
{
    private readonly IApplicationDbContext context;

    public DeleteFeedbackCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Feedbacks
                         .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                     ?? throw new NotFoundException(nameof(Feedback), request.Id);


        context.Feedbacks.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}