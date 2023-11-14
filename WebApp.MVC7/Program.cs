using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.MVC7.FluentValidation;
using WebApp.Repositories;
using WebApp.Services;
using WebApp.Services.Interfaces;

namespace WebApp.MVC7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<ArticlesAggregatorDbContext>(opt =>
                opt.UseSqlServer(connectionString));

            var logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo
                .File("log.txt", rollingInterval: RollingInterval.Day, 
                    restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo
                .Console()
                .Enrich.FromLogContext()

                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
            //builder.Host.UseSerilog((context, configuration) =>
            //    configuration.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext());

            //builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
            //builder.Services.AddScoped<IArticleSourceRepository, ArticleSourceRepository>();

            builder.Services
                .AddValidatorsFromAssemblyContaining<UserRegisterValidator>();


            builder.Services.AddScoped<IRepository<Article>, Repository<Article>>();
            builder.Services.AddScoped<IRepository<ArticleSource>, Repository<ArticleSource>>();
            builder.Services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            builder.Services.AddScoped<IRepository<User>, Repository<User>>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IArticleService, ArticleService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            //app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
          
            app.UseAuthorization();
            app.Map("/NotFound", () => new NotFoundResult());
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action:alpha=Index}/{id?}"); //controller/action/id
            // /Home/Index

            //app.MapControllerRoute(
            //    name: "custom",
            //    pattern: "{action:alpha=Index}/{controller}/{name}"); // /Index/Home
            //analogue /home/index?name="xxx"

            //domain/controllerName/ActionName
            
            app.Run();
        }
    }
}