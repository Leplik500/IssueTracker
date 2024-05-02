using IssueTracker.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<UserEntity> Users { get; set; }
}