namespace Ledgers.Data;

using Microsoft.EntityFrameworkCore;

using Ledgers.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Invoice> Invoice => Set<Invoice>();
}