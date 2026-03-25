using Aplication.Dtos.Orders;
using Aplication.Interfaces;
using Aplication.Queries.Orders.GetOrderListForManager;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Orders.GetOrderListForDefaultUser
{
    public class GetAllOrderQueryHandlerForDefaultUser : IRequestHandler<GetAllOrderQueryForDefaultUser, OrderListVm>
    {
        private readonly IMapper mapper;
        private readonly IProductsDbContext context;

        public GetAllOrderQueryHandlerForDefaultUser(IMapper mapper, IProductsDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<OrderListVm> Handle(GetAllOrderQueryForDefaultUser request, CancellationToken cancellationToken)
        {
            var orders = await context.Orders
               .Include(u => u.User)
               .Include(u => u.Product)
               .Include(u => u.StatusOrder)
               .OrderBy(x => x.Id)
               .Where(x => (x.StatusOrder.StatusName == nameof(StatusOrderEnum.PlaceAn)
                    || x.StatusOrder.StatusName == nameof(StatusOrderEnum.InProgress)
                    || x.StatusOrder.StatusName == nameof(StatusOrderEnum.ReadyForIssue))
                    && x.CustomerId== request.CurrentUserId)
               .ProjectTo<OrderLookupDto>(mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);

            return new OrderListVm { Orders = orders };
        }
    }
}
