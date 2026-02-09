using Aplication.Dtos.Feedbacks;
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

namespace Aplication.Queries.Feedbacks.GetFeedbackList
{
    public class GetAllFeedbackQueryHandler : IRequestHandler<GetAllFeedbackQuery, FeedbackListVm>
    {
        private readonly IProductsDbContext context;
        private readonly IMapper mapper;

        public GetAllFeedbackQueryHandler(IProductsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<FeedbackListVm> Handle(GetAllFeedbackQuery request, CancellationToken cancellationToken)
        {
            var feedbackQuery = await context.Feedbacks.Include(f=>f.User)
            .ProjectTo<FeedbackLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

            return new FeedbackListVm { Feedbacks = feedbackQuery };

        }
    }
}
