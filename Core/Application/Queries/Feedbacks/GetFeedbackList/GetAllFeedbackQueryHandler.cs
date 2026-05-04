using Application.Dtos.Feedbacks;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Feedbacks.GetFeedbackList;

public class GetAllFeedbackQueryHandler : IRequestHandler<GetAllFeedbackQuery, FeedbackListVm>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllFeedbackQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<FeedbackListVm> Handle(GetAllFeedbackQuery request, CancellationToken cancellationToken)
    {
        var feedbackQuery = await context.Feedbacks.Include(f => f.User)
            .ProjectTo<FeedbackLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new FeedbackListVm { Feedbacks = feedbackQuery };
    }
}