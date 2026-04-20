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
        private readonly IFileStorageService fileStorage;

        public CreateFeedbackCommandHandler(IProductsDbContext context, IFileStorageService fileStorage)
        {
            this.context = context;
            this.fileStorage = fileStorage;
        }
        public async Task<Guid> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            string? imagePath = null;

            if (request.Image != null)
            {
                imagePath = await fileStorage.SaveFileAsync(request.Image, "reviews");
            }

            var newFeedback = new Feedback
            {
                Id = Guid.NewGuid(),
                UserId = request.CurrentUserId,
                Comment = request.Comment,
                Raiting = request.Raiting,
                ImagePath = imagePath
            };

            await context.Feedbacks.AddAsync( newFeedback,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return newFeedback.Id;
        }
    }
}
