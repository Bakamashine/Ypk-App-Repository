using Aplication.Commands.Users.DeleteUser;
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

namespace Aplication.Commands.Ypks.DeleteYpk
{
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
}

