using Aplication.Dtos.Orders;
using Aplication.Dtos.Products;
using Aplication.Interfaces;
using Aplication.Queries.Products.GetProductLis;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Orders.GetOrderListForManagerHistory
{
    public class GetAllOrderQueryHandlerForManagerHistory : IRequestHandler<GetAllOrderQueryForManagerHistory, OrderListVm>
    {
        private readonly IMapper mapper;
        private readonly IProductsDbContext context;

        public GetAllOrderQueryHandlerForManagerHistory(IMapper mapper, IProductsDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<OrderListVm> Handle(GetAllOrderQueryForManagerHistory request, CancellationToken cancellationToken)
        {
            var currentUser = await context.Users.FirstOrDefaultAsync(x=>x.Id == request.CurrentUserId, cancellationToken);

            var orders = await context.Orders
               .Include(u => u.User)
               .Include(u => u.Product)
               .Include(u => u.StatusOrder)
               .OrderBy(x => x.Id)
               .Where(x=>(x.StatusOrder.StatusName == nameof(StatusOrderEnum.Adopted) 
                    || x.StatusOrder.StatusName == nameof(StatusOrderEnum.Cancelled))
                    && x.Product.YpkId == currentUser.YpkId)
               .ProjectTo<OrderLookupDto>(mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);

            return new OrderListVm { Orders = orders };
        }
    }
}