using AccountService.MediatR;
using Common.Infrastructure;

namespace AccountService.Infrastructure;


public static class DependencyServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddService<CreateAtmRequest,CreateAtmHandler>();

        return services;
    }
}