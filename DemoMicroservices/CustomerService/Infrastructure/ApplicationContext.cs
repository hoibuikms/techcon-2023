using Common.Infrastructure;
using ConsumerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsumerAPI.Infrastructure;

public class ApplicationContext : DbContext, IApplicationDbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    public virtual DbSet<RequestEntity> Request { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasPostgresExtension("unaccent");



        builder.Entity<RequestEntity>(entity => { entity.HasKey(e => e.Id); });
    }

}