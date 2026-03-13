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
        private readonly IProductsDbContext _context;
        private readonly IFileStorageService _fileStorage;


        public UpdateProductCommandHandler(
            IProductsDbContext context,
            IFileStorageService fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            string? photoPath = null;

            // Сохраняем фото, если оно есть
            if (request.Photo != null)
            {
                photoPath = await _fileStorage.SaveFileAsync(request.Photo, "products");
            }

            var entity = await _context.Products
                .Include(u => u.User)
                .Include(u => u.Ypk)
                .Include(u => u.StatusProduct)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Product), request.Id);

            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

            if (entity.UserId == request.CurrentUserId && user.Role.RoleName.Contains(nameof(EnumRoles.Admin)))
            {
                if (!string.IsNullOrEmpty(request.ProductName))
                    entity.ProductName = request.ProductName;
                if (!string.IsNullOrEmpty(request.ProductInfo))
                    entity.ProductInfo = request.ProductInfo;
                if (!string.IsNullOrEmpty(request.Adress))
                    entity.ProductInfo = request.Adress;
                if (!string.IsNullOrEmpty(photoPath))
                    entity.ProductInfo = photoPath;
                if (request.IsProduct != request.IsProduct)
                    entity.IsProduct = request.IsProduct;
                if (request.ProductCost != request.ProductCost)
                    entity.ProductCost = request.ProductCost;
                if(request.StatusProductId != Guid.Empty)
                    entity.StatusProductId = request.StatusProductId;
                if(request.YpkId != Guid.Empty)
                    entity.YpkId = request.YpkId;  

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            throw new AccessException();
        }
    }
}