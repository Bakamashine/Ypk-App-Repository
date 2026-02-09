using Aplication.Dtos.StatusOrders;
using Aplication.Dtos.StatusProducts;
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

namespace Aplication.Queries.StatusProducts.GetStatusProductsList
{
    public class GetAllStatusProductQueryHandler : IRequestHandler<GetAllStatusProductQuery, StatusProductListVm>
    {
        private readonly IProductsDbContext context;
        private readonly IMapper mapper;

        public GetAllStatusProductQueryHandler(IProductsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<StatusProductListVm> Handle(GetAllStatusProductQuery request, CancellationToken cancellationToken)
        {
            var statusProductLookupDto = await context.StatusProducts
           .ProjectTo<StatusProductLookupDto>(mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);

            return new StatusProductListVm { StatusProducts = statusProductLookupDto };
        }
    }
}
