using System.ComponentModel.DataAnnotations;
using Api.Modules.Product;
using Microsoft.AspNetCore.Identity;

namespace Api;

// This is our ApiUser, we can modify this if we need
// to add custom properties to the user
public class AppUser : IdentityUser
{
    public ICollection<ProductSeller> ProductSellers { get; }
    public ICollection<ProductComment> ProductComments { get; }
    public ICollection<ProductStock> ProductStocks { get; }
}

// This is the DTO used to exchange username and password details to 
// the create user and token endpoints
public class UserInfo
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}