using Microsoft.EntityFrameworkCore;

namespace ownMineDatabase;

public class OwnMineDbContext : DbContext {
    
    public OwnMineDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<MinecraftServerInstance> MinecraftServerInstances { get; set; }
    
}