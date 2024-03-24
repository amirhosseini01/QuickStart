using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Server.Data;

public class ServerDbContext : BaseDbContext
{
    public ServerDbContext(DbContextOptions<ServerDbContext> options) : base(options)
    {

    }
}