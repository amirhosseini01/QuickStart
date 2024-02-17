
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Shared;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbContext _context;
    private readonly DbSet<T> entities;

	public GenericRepository(DbContext context)
	{
        _context = context;
		entities = context.Set<T>();
	}

	public async Task AddAsync(T item, CancellationToken cancellationToken = default) => await entities.AddAsync(item, cancellationToken);
	public async Task AddRangeAsync(ICollection<T> items, CancellationToken cancellationToken = default) => await entities.AddRangeAsync(items, cancellationToken);
	public async Task<T?> FirstOrDefaultAsync(int id, CancellationToken cancellationToken = default) => await entities.FirstOrDefaultAsync(x=> x.Id == id, cancellationToken);
	public void Remove(T item) => entities.Remove(item);
	public void RemoveRange(ICollection<T> items) =>  entities.RemoveRange(items);
	public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
	public void UpdateRange(ICollection<T> items) => entities.UpdateRange(items);
}