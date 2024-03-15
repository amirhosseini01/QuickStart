using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Common.Commons;

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

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, PaginatedListFilter filter, CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((filter.PageIndex - 1) * filter.TakeSize).Take(filter.TakeSize).ToListAsync(cancellationToken);
        return new PaginatedList<T>(items, count, filter);
    }
}

public class PaginatedListFilter
{
    [DefaultValue(1)]
    [Required]
    [Range(minimum: 1, maximum: int.MaxValue)]
    public int PageIndex { get; set; }

    [DefaultValue(10)]
    [Required]
    [Range(minimum: 10, maximum: 100)]
    public int TakeSize { get; set; }
}