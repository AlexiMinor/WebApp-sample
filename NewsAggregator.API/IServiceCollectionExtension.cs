using Microsoft.EntityFrameworkCore;
using WebApp.Data;
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

        services.AddScoped<ArticleMapper>();
        //return services;        //return services;
    }
}