using Aplication.Commands.Feedbacks.UpdateFeedback;
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

namespace Aplication.Commands.Orders.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IProductsDbContext context;

        public UpdateOrderCommandHandler(IProductsDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Orders
                .Include(u => u.User)
                .Include(u => u.Product)
                .Include(u => u.StatusOrder)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Order), request.Id);

            var user = await context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

            if (entity.CustomerId == request.CurrentUserId && user.Role.RoleName.Contains(nameof(EnumRoles.Admin)))
            {
                if (!string.IsNullOrEmpty(request.CustomersComment))
                    entity.CustomersComment = request.CustomersComment;
                if (!string.IsNullOrEmpty(request.CustomersComment))
                    entity.CustomersComment = request.CustomersComment;
                if (request.ExecutorId != Guid.Empty)
                    entity.ExecutorId = request.ExecutorId;
                if (request.StatusOrderId != Guid.Empty)
                    entity.StatusOrderId = request.StatusOrderId;

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            throw new AccessException();
        }
    }
}
