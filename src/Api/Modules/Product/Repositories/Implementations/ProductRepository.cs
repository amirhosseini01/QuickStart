using Api.Common;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Product;

// public class ProductRepository: GenericRepository<Product>, IProductRepository
// {
//     private readonly DbSet<Product> _entities;
// 	public ProductRepository(ApiDbContext context) : base(context) => _entities = context.Products;

// 	public async Task<List<ProductListVm>> GetProductLists(ProductListFilter filter, CancellationToken cancellationToken = default) 
//     {
//         var query = _entities.AsNoTracking();

//         return await query.MapProductList().ToListAsync(cancellationToken);
//     }
// }