using Aplication.Dtos.Products;
using Aplication.Dtos.Users;
using Aplication.Interfaces;
using Aplication.Queries.Users.GetUser;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Products.GetProduct
{
    public class GetDetailProductQueryHandler : IRequestHandler<GetDetailProductQuery, ProductLookupDto>
    {
        private readonly IProductsDbContext context;
        private readonly IMapper mapper;
        public GetDetailProductQueryHandler(IProductsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<ProductLookupDto> Handle(GetDetailProductQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Products
                .Include(p=>p.User)
                .Include(p=>p.Ypk)
                .Include(p=>p.StatusProduct)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return mapper.Map<ProductLookupDto>(entity);
        }
    }
}
