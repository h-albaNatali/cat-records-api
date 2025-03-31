using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Entities;

namespace MyProject.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Record> Records => Set<Record>();
}
