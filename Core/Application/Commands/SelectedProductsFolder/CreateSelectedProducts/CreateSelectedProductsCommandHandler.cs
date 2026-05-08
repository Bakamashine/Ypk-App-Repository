using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SelectedProductsFolder.CreateSelectedProducts
{
    public class CreateSelectedProductsCommandHandler : IRequestHandler<CreateSelectedProductsCommand, Guid>
    {
        private readonly IApplicationDbContext context;
        public CreateSelectedProductsCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> Handle(CreateSelectedProductsCommand request, CancellationToken cancellationToken)
        {
            var newSelectedProducts = new SelectedProducts
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                UserId = request.CurrentUserId
            };

            await context.SelectedProducts.AddAsync(newSelectedProducts, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return newSelectedProducts.Id;
        }
    }
}
