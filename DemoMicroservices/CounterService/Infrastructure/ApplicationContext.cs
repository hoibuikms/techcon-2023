using Common.Infrastructure;
using CounterService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CounterService.Infrastructure;

public class ApplicationContext : DbContext, IApplicationDbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    public virtual DbSet<OrderEntity> Order { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasPostgresExtension("unaccent");



        builder.Entity<OrderEntity>(entity => { entity.HasKey(e => e.Id); });
    }

}