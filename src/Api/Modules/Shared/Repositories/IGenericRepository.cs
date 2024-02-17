namespace Api.Modules.Shared;

public interface IGenericRepository<T> where T: BaseEntity
{
    Task AddAsync(T item, CancellationToken cancellationToken = default);
    Task AddRangeAsync(ICollection<T> items, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync(int id, CancellationToken cancellationToken = default);
    void Remove(T item);
    void RemoveRange(ICollection<T> items);
    Task SaveChangesAsync();
    void UpdateRange(ICollection<T> items);
}