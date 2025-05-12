using Microsoft.EntityFrameworkCore;
using TheCalifornianTrail.Core.Entities;

namespace TheCalifornianTrail.Core;

public class TrailDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public TrailDbContext(DbContextOptions<TrailDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}
