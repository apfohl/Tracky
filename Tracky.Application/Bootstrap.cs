using Microsoft.Extensions.DependencyInjection;

namespace Tracky.Application;

public static class Bootstrap
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Bootstrap).Assembly));
}
