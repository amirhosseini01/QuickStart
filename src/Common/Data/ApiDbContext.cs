using Common.Modules.Cms;
using Common.Modules.Product;
using Common.Modules.Sale;
using Common.Modules.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Common.Data;

public class ApiDbContext : IdentityDbContext<AppUser>
{
    //  dotnet ef --startup-project Server migrations add ... --project Commons --context ApiDbContext
    //  dotnet ef --startup-project Server database update --project Infrastructure --context ApiDbContext
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    // products
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductComment> ProductComments { get; set; }
    public DbSet<ProductModel> ProductModels { get; set; }
    public DbSet<ProductSeller> ProductSellers { get; set; }
    public DbSet<ProductStock> ProductStocks { get; set; }

    // CMS
    public DbSet<Slider> Sliders { get; set; }

    // Sales
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    // User
    public DbSet<UserAddress> UserAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
}
