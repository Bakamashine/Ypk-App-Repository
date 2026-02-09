using Aplication.Interfaces;
using Application.Common.Exceptions;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Feedbacks.DeleteFeedback
{
    public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand>
    {
        private readonly IProductsDbContext context;

        public DeleteFeedbackCommandHandler(IProductsDbContext context)
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
}
