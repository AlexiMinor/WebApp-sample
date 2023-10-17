using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.MVC7.Services;
using WebApp.Repositories;

namespace WebApp.MVC7
{
    public class Program
    {
        public static void Main(string[] args)
        {
         const string ConnString = "Server=DESKTOP-JPGDIHT;Database=ArticleAggregator;Trusted_Connection=True;TrustServerCertificate=True;";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ArticlesAggregatorDbContext>(opt =>
                opt.UseSqlServer(ConnString));

            //builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
            //builder.Services.AddScoped<IArticleSourceRepository, ArticleSourceRepository>();

            builder.Services.AddScoped<IRepository<Article>, Repository<Article>>();
            builder.Services.AddScoped<IRepository<ArticleSource>, Repository<ArticleSource>>();
            builder.Services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            builder.Services.AddScoped<IRepository<User>, Repository<User>>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //domain/controllerName/ActionName

            //ArticleController => ArticleModel
            //Action = ActionName

            app.Run();
        }
    }
}