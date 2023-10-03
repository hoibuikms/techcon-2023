using Common.Infrastructure;
using CounterService.MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CounterService.Infrastructure;


public static class DependencyServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddService<OrderRequest,OrderHandler>();

        return services;
    }
}