using Aplication.Dtos.Products;
using Aplication.Dtos.Users;
using Aplication.Interfaces;
using Aplication.Queries.Users.GetUserList;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Products.GetCreatedProductList
{
    public class GetAllCreatedProductQueryHandler : IRequestHandler<GetAllCreatedProductQuery, ProductListVm>
    {
        private readonly IMapper mapper;
        private readonly IProductsDbContext context;

        public GetAllCreatedProductQueryHandler(IMapper mapper, IProductsDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ProductListVm> Handle(GetAllCreatedProductQuery request, CancellationToken cancellationToken)
        {
            var products = await context.Products
               .Include(u => u.User)
               .Include(u => u.StatusProduct)
               .Include(u => u.Ypk)
               .OrderBy(x => x.Id)
               .Where(x => x.StatusProduct.StatusName.Contains(nameof(StatusProductEnum.Editing)))
               .ProjectTo<ProductLookupDto>(mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);

            return new ProductListVm { Products = products };
        }
    }
}
