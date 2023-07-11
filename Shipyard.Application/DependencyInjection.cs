using Microsoft.Extensions.DependencyInjection;
using Shipyard.Application.Services;
using Shipyard.Application.Services.AdministrationServices;

namespace Shipyard.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        return services;
    }
}