using Aplication.Commands.Orders.UpdateOrder;
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

namespace Aplication.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductsDbContext context;

        public UpdateProductCommandHandler(IProductsDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Products
                .Include(u => u.User)
                .Include(u => u.Ypk)
                .Include(u => u.StatusProduct)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Product), request.Id);

            var user = await context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

            if (entity.UserId == request.CurrentUserId && user.Role.RoleName.Contains(nameof(EnumRoles.Admin)))
            {
                if (!string.IsNullOrEmpty(request.ProductName))
                    entity.ProductName = request.ProductName;
                if (!string.IsNullOrEmpty(request.ProductInfo))
                    entity.ProductInfo = request.ProductInfo;
                if (!string.IsNullOrEmpty(request.Adress))
                    entity.ProductInfo = request.Adress;
                if (!string.IsNullOrEmpty(request.Photo))
                    entity.ProductInfo = request.Photo;
                if (request.IsProduct != request.IsProduct)
                    entity.IsProduct = request.IsProduct;
                if (request.ProductCost != request.ProductCost)
                    entity.ProductCost = request.ProductCost;
                if(request.StatusProduct != null)
                    entity.StatusProductId = request.StatusProduct.Id;
                if(request.Ypk != null)
                    entity.YpkId = request.Ypk.Id;  

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            throw new AccessException();
        }
    }
}