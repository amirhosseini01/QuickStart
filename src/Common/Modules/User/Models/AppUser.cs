﻿using Common.Modules.Product;
using Common.Modules.Sale;
using Microsoft.AspNetCore.Identity;

namespace Common.Modules.User;

// This is our ApiUser, we can modify this if we need
// to add custom properties to the user
public class AppUser : IdentityUser
{
    public ICollection<ProductSeller> ProductSellers { get; }
    public ICollection<ProductComment> ProductComments { get; }
    public ICollection<ProductStock> ProductStocks { get; }
    public ICollection<Order> Orders { get; }
}