using Microsoft.EntityFrameworkCore;
using SizCardApi.Models;

namespace SizCardApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<SizCard> SizCards => Set<SizCard>();
    public DbSet<WriteOffAct> WriteOffActs => Set<WriteOffAct>();
    public DbSet<WriteOffActItem> WriteOffActItems => Set<WriteOffActItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Номер акта уникален.
        modelBuilder.Entity<WriteOffAct>()
            .HasIndex(a => a.ActNumber)
            .IsUnique();
    }
}
