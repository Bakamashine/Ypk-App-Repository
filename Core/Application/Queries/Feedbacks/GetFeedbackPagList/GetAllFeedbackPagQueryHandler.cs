using Application.Dtos.Feedbacks;
using Application.Extensions;
using Application.Interfaces;
using Application.Queries.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Queries.Feedbacks.GetFeedbackPagList;

public class GetAllFeedbackPagQueryHandler : IRequestHandler<GetAllFeedbackPagQuery, PagedList<FeedbackLookupDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAllFeedbackPagQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    
    public async Task<PagedList<FeedbackLookupDto>> Handle(GetAllFeedbackPagQuery request, CancellationToken ct)
    {
        var query = context.Feedbacks
            .ProjectTo<FeedbackLookupDto>(mapper.ConfigurationProvider)
            .AsQueryable();
        return await query.ToPagedListAsync(request.Page, request.PageSize, ct);    
    }
    
}