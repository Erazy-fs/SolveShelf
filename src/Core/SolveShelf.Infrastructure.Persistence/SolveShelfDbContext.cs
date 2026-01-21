using Microsoft.EntityFrameworkCore;
using SolveShelf.Domain;

namespace SolveShelf.Infrastructure.Persistence;

public sealed class SolveShelfDbContext : DbContext
{
    public SolveShelfDbContext(DbContextOptions<SolveShelfDbContext> options) : base(options) { }

    public DbSet<Problem> Problems => Set<Problem>();
    public DbSet<Solution> Solutions => Set<Solution>();
    public DbSet<Language> Languages => Set<Language>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SolveShelfDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
