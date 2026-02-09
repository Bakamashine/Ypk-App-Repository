using Aplication.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Feedbacks.CreateFeedback
{
    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, Guid>
    {
        private readonly IProductsDbContext context;

        public CreateFeedbackCommandHandler(IProductsDbContext context)
        {
            this.context = context;
        }
        public async Task<Guid> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var newFeedback = new Feedback
            {
                Id = Guid.NewGuid(),
                UserId = request.CurrentUserId,
                Comment = request.FeedbackName,
                Raiting = request.Raiting
            };

            await context.Feedbacks.AddAsync( newFeedback,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return newFeedback.Id;
        }
    }
}
