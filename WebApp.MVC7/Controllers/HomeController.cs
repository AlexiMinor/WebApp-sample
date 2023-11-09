using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
            //_dbInitializer = dbInitializer;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Hello from HomeController. Serilog works well");

            //ViewBag.Hello = "Hello world";
            //ViewData["Bye"] = "Bye world";
            ////return Ok("blablabla1")

            //await _dbInitializer.InitDbWithTestValues();

            return View();
        }

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