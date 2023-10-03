using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Infrastructure;

public static class Database
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        //var connectionString = Environment.GetEnvironmentVariable("WRITE_DATABASE_CONNECTION_STRING");
        var connectionString = "Host=localhost;port=5432;Database=techcon;Username=postgres;Password=postgres";

        services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork<ApplicationContext>, UnitOfWork<ApplicationContext>>();

        return services;
    }
}