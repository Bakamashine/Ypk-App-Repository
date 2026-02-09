using Aplication.Commands.Orders.CreateOrder;
using Aplication.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Ypks.CreateYpk
{
    public class CreateYpkCommandHandler : IRequestHandler<CreateYpkCommand, Guid>
    {
        private readonly IProductsDbContext context;

        public CreateYpkCommandHandler(IProductsDbContext context)
        {
            this.context = context;
        }
        public async Task<Guid> Handle(CreateYpkCommand request, CancellationToken cancellationToken)
        {
            var newYpk = new Ypk
            {
                Id = Guid.NewGuid(),
                YpkName = request.YpkName,
                IsActive = true
            };

            await context.Ypks.AddAsync(newYpk, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return newYpk.Id;
        }
    }
}
