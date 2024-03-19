﻿using Common.Modules.Cms;
using Common.Modules.Product;
using Common.Modules.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Common.Data;

public class ApiDbContext : IdentityDbContext<AppUser>
{
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
    protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
}