using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Shared;

public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    public bool HasPreviousPage => this.PageIndex > 1;
    public bool HasNextPage => this.PageIndex < this.TotalPages;

    public PaginatedList(IEnumerable<T> items, int count, PaginatedListFilter filter)
    {
        this.PageIndex = filter.PageIndex;
        this.TotalPages = (int)Math.Ceiling(count / (double)filter.PageSize);

        this.AddRange(items);
    }

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, PaginatedListFilter filter)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        return new PaginatedList<T>(items, count, filter);
    }
}

public class PaginatedListFilter
{
    public int PageIndex {get; set;}
    public int PageSize {get; set;}
}