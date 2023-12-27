﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class ApiDbContext : IdentityDbContext<ApiUser>
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
    }
}