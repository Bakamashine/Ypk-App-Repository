using Application.Dtos.SelectedProductsFolder;
using Application.Dtos.StatusOrders;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.SelectedProductsFolder.GetSelectedProductsList
{
    public class GetAllSelectedProductsQueryHandler : IRequestHandler<GetAllSelectedProductsQuery, SelectedProductsListVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetAllSelectedProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<SelectedProductsListVm> Handle(GetAllSelectedProductsQuery request, CancellationToken cancellationToken)
        {
            var query = await context.SelectedProducts.Where(x => x.UserId == request.CurrentUserId)
                .Include(x => x.Product)
                .Include(x => x.User)
                .ProjectTo<SelectedProductsLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

            return new SelectedProductsListVm { SelectedProducts = query };
        }
    }
}
