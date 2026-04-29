namespace Application.Queries.Base;

public class PagedList<T>
{
    public IList<T> Items { get; set; } = new List<T>();
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    // public string? SortBy { get; set; }
    public int? PageCount { get; set; }
    
    public PagedList()
    {
        
    }

    public PagedList(IList<T> items, int pageCount, int page, int pageSize)
    {
        Items = items;
        PageCount = pageCount;
        Page = page;
        PageSize = pageSize;
    }
}