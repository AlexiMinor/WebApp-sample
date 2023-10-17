using Microsoft.AspNetCore.Mvc;
using WebApp.MVC7.Models;
using WebApp.Repositories;

namespace WebApp.MVC7.Controllers
{
    //[NonController]
    public class ArticleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var articlesList = _unitOfWork.ArticleRepository.GetArticlesWithSource()
                .Select(article => new ArticleModel()
                    {
                        Id = article.Id,
                        Date = article.Date,
                        Rate = article.Rate,
                        Title = article.Title,
                        Source = article.ArticleSource.Name,
                        Description = article.Description
                    })
                .ToList();
            return View(articlesList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(ArticleModel articleModel)
        //{
        //    _unitOfWork.ArticleRepository.MakeChanges();
        //    _unitOfWork.ArticleSourceRepository.MakeChanges();
        //    await _unitOfWork.Commit();
        //    return Ok();

        //}

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id.HasValue)
            {
                var article = await _unitOfWork.ArticleRepository.GetById(id.Value);
                if (article != null)
                {
                    var articleModel = new ArticleModel()
                    {
                        Id = article.Id,
                        Date = article.Date,
                        Rate = article.Rate,
                        Title = article.Title,
                        Source = article.ArticleSource.Name,
                        Description = article.Description,
                    };
                    return View(articleModel);
                }
            }
            return BadRequest();
        }

        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    if (id.HasValue && _articles.ContainsKey(id.Value))
        //    {
        //        var article = _articles[id.Value];
        //        return View(article);
        //    }

        //    return BadRequest();
        //}

        //[HttpPost]
        //public IActionResult Edit([FromRoute] int? id, [FromForm] ArticleModel articleModel)
        //{
        //    if (id.HasValue && _articles.ContainsKey(id.Value))
        //    {
        //        _articles[id.Value].Title = articleModel.Title;
        //        return RedirectToAction("Details", new {id = id.Value});
        //    }

        //    return View();
        //}

        //[ActionName("Welcome")]
        [HttpPost]
        public IActionResult Favorite()
        {
            return Ok("Hello World");
        }

        //[NonAction]
        private IActionResult Do()
        {
            return Ok(321);
        }
    }
}
