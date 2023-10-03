using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProductService.Entities;

namespace ProductService.Infrastructure;

public class ApplicationContext : DbContext, IApplicationDbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<CategoryEntity> Category { get; set; }
    public virtual DbSet<ServiceEntity> Service { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasPostgresExtension("unaccent");



        builder.Entity<CategoryEntity>(entity => { entity.HasKey(e => e.Id); });
        builder.Entity<ServiceEntity>(entity => { entity.HasKey(e => e.Id); });
    }
}