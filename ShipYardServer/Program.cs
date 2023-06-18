using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using ShipYardServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShipYardServer.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("SqlServerDb")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

SetUpBearerJwtAuth(builder);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

static void SetUpBearerJwtAuth(WebApplicationBuilder builder)
{
    //Fetch JWT configuration from appsettings.json
    var jwtSection = builder.Configuration.GetSection("jwtBearerTokenSettings");
    //Parse jwtSection from appsettings.json into Concrete Class "JWTBearerTokenSettings"
    builder.Services.Configure<JWTBearerTokenSettings>(jwtSection);
    var jwtBearerTokenSettings = jwtSection.Get<JWTBearerTokenSettings>();
    var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

    builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = jwtBearerTokenSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtBearerTokenSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
}
