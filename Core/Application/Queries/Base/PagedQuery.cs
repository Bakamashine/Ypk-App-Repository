using MediatR;

namespace Application.Queries.Base;

public abstract class PagedQuery<TResponse>: IRequest<PagedList<TResponse>>
{
    public int Page { get; set; } = 1;
    public int PageSize { set; get; } = 5;

}