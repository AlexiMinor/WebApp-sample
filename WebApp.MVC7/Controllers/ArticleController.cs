using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Core;
using WebApp.Data.Entities;
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
        public async Task<IActionResult> Index()
        {
            var articlesList = await _unitOfWork.ArticleRepository
                .FindBy(article => !string.IsNullOrEmpty(article.Title),
                    article => article.ArticleSource)
                .Select(article => new ArticleModel()
                {
                    Id = article.Id,
                    Date = article.Date,
                    Rate = article.Rate,
                    Title = article.Title,
                    Source = article.ArticleSource.Name,
                    Description = article.Description,
                })
                .ToListAsync();
            return View(articlesList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleModel articleModel)
        {
            
            await _unitOfWork.Commit();
            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id.HasValue)
            {
                var article = await _unitOfWork.ArticleRepository.GetById(id.Value, art => art.ArticleSource);
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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var article = await _unitOfWork.ArticleRepository.GetByIdAsNoTracking(id);
            if (article != null)
            {
                var articleModel = new ArticleModel()
                {
                    Id = article.Id,
                    Title = article.Title,
                };
                return View(articleModel);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] ArticleModel articleModel)
        {
            if (await _unitOfWork.ArticleRepository.GetByIdAsNoTracking(articleModel.Id) != null)
            {
                await _unitOfWork.ArticleRepository.Patch(articleModel.Id, new List<PatchDto>()
                {
                    //should be sure that name of property/field in model same with property name of entity
                    new() { PropertyName = nameof(articleModel.Title), PropertyValue = articleModel.Title }
                });
                await _unitOfWork.Commit();
                return RedirectToAction("Details", new { id = articleModel.Id });
            }

            return View();
        }

        //[ActionName("Welcome")]
        [HttpGet]
        public async Task<IActionResult> Favorite()
        {
            var articlesList = await _unitOfWork.ArticleRepository
                .FindBy(article => !string.IsNullOrEmpty(article.Title),
                    article => article.ArticleSource)
                .OrderBy(article => article.Date)
               
                .Select(article => new ArticleModel()
                {
                    Id = article.Id,
                    Date = article.Date,
                    Rate = article.Rate,
                    Title = article.Title,
                    Source = article.ArticleSource.Name,
                    Description = article.Description,
                })
                .FirstOrDefaultAsync();

            return View(articlesList);
        }

        //[NonAction]
        private IActionResult Do()
        {
            return Ok(321);
        }
    }
}
