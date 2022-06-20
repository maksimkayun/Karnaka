using Karnaka.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Karnaka.Data;

public class KarnakaContext : DbContext
{
    protected KarnakaContext()
    {
    }

    public KarnakaContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Conspirator>()
            .HasOne(e => e.Location).WithMany(e => e.Conspirators)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Location>()
            .HasMany(e => e.Conspirators).WithOne(e => e.Location)
            .OnDelete(DeleteBehavior.SetNull);
    }

    public DbSet<Conspirator> Conspirators { get; set; }
    public DbSet<PartPlan> PartPlans { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<ErrorCode> ErrorCodes { get; set; }
}