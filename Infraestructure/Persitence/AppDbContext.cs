using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persitence;

public partial class AppDbContext : DbContext
{
    //Requerido para funcionamiento
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}