using Aplication.Dtos.Orders;
using Aplication.Dtos.Products;
using Aplication.Interfaces;
using Aplication.Queries.Products.GetProduct;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Orders.GetOrder
{
    public class GetDetailOrderQueryHandler : IRequestHandler<GetDetailOrderQuery, OrderLookupDto>
    {
        private readonly IProductsDbContext context;
        private readonly IMapper mapper;
        public GetDetailOrderQueryHandler(IProductsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<OrderLookupDto> Handle(GetDetailOrderQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Orders
                .Include(p => p.User)
                .Include(p => p.Product)
                .Include(p => p.StatusOrder)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return mapper.Map<OrderLookupDto>(entity);
        }
    }
}
