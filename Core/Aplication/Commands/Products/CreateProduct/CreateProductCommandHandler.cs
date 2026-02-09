using Aplication.Commands.Orders.CreateOrder;
using Aplication.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductsDbContext context;

        public CreateProductCommandHandler(IProductsDbContext context)
        {
            this.context = context;
        }
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                UserId = request.CurrentUserId,
                ProductName = request.ProductName,
                YpkId = request.Ypk.Id,
                StatusProductId = request.StatusProduct.Id,
                ProductCost = request.ProductCost,
                ProductInfo = request.ProductInfo,
                IsProduct = request.IsProduct,
                Photo = request.Photo,
                Adress = request.Adress,
                Raiting = request.Raiting

            };

            await context.Products.AddAsync(newProduct, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return newProduct.Id;
        }
    }
}