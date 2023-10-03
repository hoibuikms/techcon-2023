using ProductService.Queries;
using Common.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ProductService.Infrastructure;

public static class DependencyServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddService<GetCategoryRequest,GetCategoryHandler>();

        return services;
    }
}