using Api.Modules.Product;
using Microsoft.AspNetCore.Identity;

namespace Api.Modules.Users;

// This is our ApiUser, we can modify this if we need
// to add custom properties to the user
public class AppUser : IdentityUser
{
    public ICollection<ProductSeller> ProductSellers { get; }
    public ICollection<ProductComment> ProductComments { get; }
    public ICollection<ProductStock> ProductStocks { get; }
}