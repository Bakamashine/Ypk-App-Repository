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

namespace Aplication.Commands.Products.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductsDbContext context;

        public DeleteProductCommandHandler(IProductsDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Products
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                 ?? throw new NotFoundException(nameof(Product), request.Id);

            entity.StatusProduct = context.StatusProducts.FirstOrDefault(st => st.StatusName.Contains(nameof(StatusProductEnum.Deleted)));
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

