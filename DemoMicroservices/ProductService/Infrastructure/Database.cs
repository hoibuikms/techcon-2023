using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Infrastructure;

public static class Database
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        //var connectionString = Environment.GetEnvironmentVariable("WRITE_DATABASE_CONNECTION_STRING");
        var connectionString = "Host=ep-tiny-star-42894873.ap-southeast-1.aws.neon.tech;port=5432;Database=product;Username=hoibui;Password=oY1vxNaqhip9";

        services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork<ApplicationContext>, UnitOfWork<ApplicationContext>>();

        return services;
    }
}