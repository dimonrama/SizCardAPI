using Microsoft.EntityFrameworkCore;
using SizCardApi.Models;

namespace SizCardApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<SizCard> SizCards => Set<SizCard>();
}
