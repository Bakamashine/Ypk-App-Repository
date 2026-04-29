using Application.Queries.Base;
using Microsoft.EntityFrameworkCore;

namespace Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> query, int page, int pageSize,  CancellationToken ct)
    {
        var totalCount = await query.CountAsync(ct);
        
        IList<T> items = await query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

        var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
        return new PagedList<T>(items, pageCount, page, pageSize);
    }
}