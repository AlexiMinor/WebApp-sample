using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApp.Data;
using WebApp.Data.CQS.Commands;
using WebApp.Data.Entities;
using WebApp.Mappers;
using WebApp.Repositories;
using WebApp.Services;
using WebApp.Services.Interfaces;

namespace WebApp.WebApi;

public static class ServiceCollectionExtension
{
    public static void RegisterServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<ArticlesAggregatorDbContext>(opt =>
            opt.UseSqlServer(connectionString));

        services.AddScoped<IRepository<Article>, Repository<Article>>();
        services.AddScoped<IRepository<ArticleSource>, Repository<ArticleSource>>();
        services.AddScoped<IRepository<Comment>, Repository<Comment>>();
        services.AddScoped<IRepository<Role>, Repository<Role>>();
        services.AddScoped<IRepository<User>, Repository<User>>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<ArticleMapper>();
        services.AddScoped<UserMapper>();

        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(connectionString));

        services.AddHangfireServer();

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(AddArticleCommand).Assembly);
        });
    }

    public static void ConfigureJwt
        (this IServiceCollection services, IConfiguration configuration)
    {
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var secret = configuration["Jwt:Secret"];
        

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    
                };
            });
    }
}