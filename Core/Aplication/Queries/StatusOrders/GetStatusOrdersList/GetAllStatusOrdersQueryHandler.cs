using Aplication.Dtos.StatusOrders;
using Aplication.Dtos.Ypks;
using Aplication.Interfaces;
using Aplication.Queries.Ypks.GetYpkList;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.StatusOrders.GetStatusOrdersList
{
    public class GetAllStatusOrdersQueryHandler : IRequestHandler<GetAllStatusOrdersQuery, StatusOrderListVm>
    {
        private readonly IProductsDbContext context;
        private readonly IMapper mapper;

        public GetAllStatusOrdersQueryHandler(IProductsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<StatusOrderListVm> Handle(GetAllStatusOrdersQuery request, CancellationToken cancellationToken)
        {
            var statusOrdersQuery = await context.StatusOrders
           .ProjectTo<StatusOrderLookupDto>(mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);

            return new StatusOrderListVm { StatusOrders = statusOrdersQuery };
        }
    }
}
