using Aplication.Commands.Feedbacks.CreateFeedback;
using Aplication.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Orders.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IProductsDbContext context;

        public CreateOrderCommandHandler(IProductsDbContext context)
        {
            this.context = context;
        }
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newOrder = new Order
            {
                Id = Guid.NewGuid(),
                UserId = request.CurrentUserId,
                ProductId = request.Product.Id,
                StatusOrderId = request.StatusOrder.Id,
                Date = DateTime.UtcNow,
                CustomersComment = request.CustomersComment,
                UserComment= request.UserComment,

            };

            await context.Orders.AddAsync(newOrder, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return newOrder.Id;
        }
    }
}

