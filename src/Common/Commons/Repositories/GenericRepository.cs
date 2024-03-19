
using Microsoft.EntityFrameworkCore;

namespace Common.Commons;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
	private readonly DbContext _context;
	private readonly DbSet<T> entities;

	public GenericRepository(DbContext context)
	{
		_context = context;
		entities = context.Set<T>();
	}

	public async Task AddAsync(T item, CancellationToken ct = default) =>
		await entities.AddAsync(item, ct);
	public async Task AddRangeAsync(ICollection<T> items, CancellationToken ct = default) =>
		await entities.AddRangeAsync(items, ct);
	public async Task<T?> FirstOrDefaultAsync(object id, CancellationToken ct = default) =>
		await entities.FindAsync(id, ct);
	public void Remove(T item) => entities.Remove(item);
	public void RemoveRange(ICollection<T> items) => entities.RemoveRange(items);
	public async Task SaveChangesAsync(CancellationToken ct = default) =>
		await _context.SaveChangesAsync();
	public void UpdateRange(ICollection<T> items) => entities.UpdateRange(items);
}