﻿using Api.Modules.Product;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class ApiDbContext : IdentityDbContext<AppUser>
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    // products
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductComment> ProductComments { get; set; }
    public DbSet<ProductGallery> ProductGalleries { get; set; }
    public DbSet<ProductModel> ProductModels { get; set; }
    public DbSet<ProductProperty> ProductProperties { get; set; }
    public DbSet<ProductPropertyValue> ProductPropertyValues { get; set; }
    public DbSet<ProductSeller> ProductSellers { get; set; }
    public DbSet<ProductStock> ProductStocks { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
    }
}
