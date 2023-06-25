using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shipyard.Application.Common.Interfaces.Authentication;
using Shipyard.Application.Common.Interfaces.Persistence;
using Shipyard.Application.Common.Services;
using Shipyard.Infrastructure.Authentication;
using Shipyard.Infrastructure.Persistence;
using Shipyard.Infrastructure.Persistence.Repositories;
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

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddTransient<IShipRepository, ShipRepository>();
        services.AddDbContext<ShipDbContext>(options => options.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=kd100817;Database=shipyard_dev;"));
        services.AddDbContext<UserDbContext>(options => options.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=kd100817;Database=shipyard_dev;"));

        return services;
    }
}