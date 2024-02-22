using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Api.Common;

public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalCount { get; }
    public int PageSize { get; }
    public bool HasPreviousPage => this.PageIndex > 1;
    public bool HasNextPage => this.PageIndex < this.TotalPages;


    public PaginatedList(IEnumerable<T> items, int count, PaginatedListFilter filter)
    {
        this.PageIndex = filter.PageIndex;
        this.TotalCount = count;
        this.TotalPages = (int)Math.Ceiling(count / (double)filter.TakeSize);
        this.PageSize = Math.Min(this.PageIndex * this.TotalPages, count);
        this.AddRange(items);
    }

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, PaginatedListFilter filter)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((filter.PageIndex - 1) * filter.TakeSize).Take(filter.TakeSize).ToListAsync();
        return new PaginatedList<T>(items, count, filter);
    }
}

public class PaginatedListFilter
{
    [Range(minimum: 1, maximum: int.MaxValue)]
    public int PageIndex {get; set;} = 1;
    
    [Range(minimum: 1, maximum: 100)]
    public int TakeSize {get; set;} = 10;
}