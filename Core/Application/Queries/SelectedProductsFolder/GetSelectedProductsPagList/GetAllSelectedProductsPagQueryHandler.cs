using Application.Dtos.SelectedProductsFolder;
using Application.Dtos.StatusOrders;
using Application.Extensions;
using Application.Interfaces;
using Application.Queries.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.SelectedProductsFolder.GetSelectedProductsPagList
{
    public class GetAllSelectedProductsPagQueryHandler : IRequestHandler<GetAllSelectedProductsPagQuery, PagedList<SelectedProductsLookupDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetAllSelectedProductsPagQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PagedList<SelectedProductsLookupDto>> Handle(GetAllSelectedProductsPagQuery request, CancellationToken cancellationToken)
        {
            var query = context.SelectedProducts.Where(x=> x.UserId == request.CurrentUserId)
                .Include(x=>x.Product)
                .Include(x=>x.User)
                .ProjectTo<SelectedProductsLookupDto>(mapper.ConfigurationProvider)
                .AsQueryable();

            return await query.ToPagedListAsync(request.Page, request.PageSize, cancellationToken);
        }
    }
}
