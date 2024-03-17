using System.Reflection;
using LinkShortener.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Infrastructure.Daos;

public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }
    
    public virtual DbSet<Url> Urls { get; set; }
    
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Scans a given assembly for all types that implement IEntityTypeConfiguration, and registers each one automatically
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer();
        }
    }
}