using IssueTracker.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.DAL;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<IssueEntity> Issues { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IssueEntity>()
            .HasMany(i => i.Assignees)
            .WithMany()
            .UsingEntity(j => j.ToTable("Assignees"));
        base.OnModelCreating(modelBuilder);
    }
}