using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using WebApp.MVC7.Filters;
using WebApp.MVC7.Models;
using WebApp.MVC7.Services;

namespace WebApp.MVC7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IDbInitializer _dbInitializer;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[TestResourceFilter(50, "Hello world")]
        [TestActionFilter]
        public async Task<IActionResult> Index(int? data)
        {
            _logger.LogInformation("Hello from HomeController. Serilog works well");

            //ViewBag.Hello = "Hello world";
            //ViewData["Bye"] = "Bye world";
            ////return Ok("blablabla1")

            //await _dbInitializer.InitDbWithTestValues();

            return View();
        }

        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Privacy()
        {
            //await _dbInitializer.InsertArticles();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}