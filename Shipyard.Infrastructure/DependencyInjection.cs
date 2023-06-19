using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shipyard.Application.Common.Interfaces.Authentication;
using Shipyard.Application.Common.Services;
using Shipyard.Infrastructure.Authentication;
using Shipyard.Infrastructure.Services;

namespace Shipyard.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}