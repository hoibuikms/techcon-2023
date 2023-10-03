using AccountService.Entities;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure;


public class ApplicationContext : DbContext, IApplicationDbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    public virtual DbSet<ATMEntity> ATM { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasPostgresExtension("unaccent");



        builder.Entity<ATMEntity>(entity => { entity.HasKey(e => e.Id); });
    }
}