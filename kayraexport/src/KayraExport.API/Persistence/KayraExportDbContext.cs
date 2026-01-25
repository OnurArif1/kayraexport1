using KayraExport.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace KayraExport.API.Persistence;

public class KayraExportDbContext : DbContext
{
    public KayraExportDbContext(DbContextOptions<KayraExportDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KayraExportDbContext).Assembly);
    }
}
