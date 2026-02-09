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

namespace Aplication.Commands.Orders.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IProductsDbContext context;

        public DeleteOrderCommandHandler(IProductsDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Orders
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                 ?? throw new NotFoundException(nameof(Order), request.Id);


            context.Orders.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}