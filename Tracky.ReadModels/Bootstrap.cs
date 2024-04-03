using Microsoft.Extensions.DependencyInjection;

namespace Tracky.ReadModels;

public static class Bootstrap
{
    public static IServiceCollection AddReadModels(this IServiceCollection services) =>
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Bootstrap).Assembly));
}
