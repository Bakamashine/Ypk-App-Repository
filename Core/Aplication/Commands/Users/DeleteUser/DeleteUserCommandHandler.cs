using Aplication.Commands.Feedbacks.DeleteFeedback;
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

namespace Aplication.Commands.Users.DeleteUser
{
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
}
