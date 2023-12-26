using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services.Interfaces;

namespace WebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IBackgroundJobClient _jobClient;
        private readonly IArticleService _articleService;

        public TestController(IBackgroundJobClient jobClient, IArticleService articleService)
        {
            _jobClient = jobClient;
            _articleService = articleService;
        }

        //[HttpPost]
        //public async Task<IActionResult> Test()
        //{
        ////{
        ////    _jobClient.Enqueue(() => Console.WriteLine("Hello"));

        ////    _jobClient.Schedule(() => Console.WriteLine("Hello from delayed"), TimeSpan.FromSeconds(30));

        ////    RecurringJob.AddOrUpdate(
        ////        "TestRecurringJob",
        ////        () => Console.WriteLine("Recurring!"), "* 8-17 * * Mon-Fri");

        //RecurringJob.AddOrUpdate("Article Aggregation", 
        //    _articleService.AggregateAllNecessaryData()
        //    );
        //    return Ok();
        //}
    }
}
