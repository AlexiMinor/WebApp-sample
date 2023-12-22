using WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleControlller : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleControlller(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _articleService.GetArticleById(id);
            return Ok(response);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetByName(string name)
        //{

        //    var response = await _articleService.GetArticlesByName(name);
        //    return Ok(response);
        //}

        [HttpGet]
        public async Task<IActionResult> GetOnlyPositive()
        {

            var response = await _articleService.GetPositive();
            return Ok(response);
        }

    }
}
