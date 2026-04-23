using Application.Common.Exceptions;
using Application.Dtos.Feedbacks;
using Application.Interfaces;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Feedbacks.GetFeedback;

public class GetDetailsFeedbackQueryHandler : IRequestHandler<GetDetailsFeedbackQuery, FeedbackLookupDto>
{
    private readonly IProductsDbContext context;
    private readonly IMapper mapper;

    public GetDetailsFeedbackQueryHandler(IProductsDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<FeedbackLookupDto> Handle(GetDetailsFeedbackQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Feedbacks.Include(f => f.User)
                         .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                     ?? throw new NotFoundException(nameof(Role), request.Id);
        return mapper.Map<FeedbackLookupDto>(entity);
    }
}