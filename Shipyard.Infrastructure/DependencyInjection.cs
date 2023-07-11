using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shipyard.Application.Common.Interfaces.Authentication;
using Shipyard.Application.Common.Interfaces.Persistence;
using Shipyard.Application.Common.Interfaces.Persistence.Administation;
using Shipyard.Application.Common.Services;
using Shipyard.Infrastructure.Authentication;
using Shipyard.Infrastructure.Persistence;
using Shipyard.Infrastructure.Persistence.Repositories;
using Shipyard.Infrastructure.Persistence.Repositories.Administration;
using Shipyard.Infrastructure.Services;

namespace Shipyard.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddDbContext<UserDbContext>(options => 
            options.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=kd100817;Database=shipyard_dev;"));

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSetting = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSetting);
        services.AddSingleton(Options.Create(jwtSetting));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSetting.Issuer,
                ValidAudience = jwtSetting.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret))
            });
        return services;
    }
}