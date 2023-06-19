using Microsoft.Extensions.DependencyInjection;
using Shipyard.Application.Services;

namespace Shipyard.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}