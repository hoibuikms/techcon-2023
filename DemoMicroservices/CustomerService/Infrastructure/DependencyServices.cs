using ConsumerAPI.MediatR;
using Microsoft.Extensions.DependencyInjection;
using Common.Infrastructure;

namespace ConsumerAPI.Infrastructure;

public static class DependencyServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddService<CreateRequest, CreateRequestHandler>();
        services.AddService<UpdateRequest, UpdateHandler>();
        services.AddService<GetRequest, GetHandler>();

        return services;
    }
}