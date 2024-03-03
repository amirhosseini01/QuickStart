namespace Api.Common;

public interface IGenericRepository<T>
{
    Task AddAsync(T item, CancellationToken cancellationToken = default);
    Task AddRangeAsync(ICollection<T> items, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync(object id, CancellationToken cancellationToken = default);
    void Remove(T item);
    void RemoveRange(ICollection<T> items);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    void UpdateRange(ICollection<T> items);
}