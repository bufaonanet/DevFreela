using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}