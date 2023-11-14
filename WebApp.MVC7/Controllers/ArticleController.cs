using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Core;
using WebApp.Data.Entities;
using WebApp.MVC7.Filters;
using WebApp.MVC7.Models;
using WebApp.Repositories;
using WebApp.Services.Interfaces;

namespace WebApp.MVC7.Controllers
{
    //[NonController]
    [LastVisitTrackerResourceFilter]
    public class ArticleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArticleService _articleService;
        private readonly ILogger<ArticleController> _logger;
        public ArticleController(IUnitOfWork unitOfWork, 
            ILogger<ArticleController> logger, 
            IArticleService articleService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _articleService = articleService;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Selecting all articles started");
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
                _logger.LogInformation("Selecting all articles finished");

                return View(articlesList);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);//should be replaced for friendly page
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateArticleModel()
            {
                AvailableSources = await _unitOfWork.ArticleSourceRepository
                    .GetAsQueryable()
                    .AsNoTracking()
                    .Select(source 
                        => new SelectListItem(source.Name, source.Id.ToString("D")))
                    .ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleModel articleModel)
         {
             if (ModelState.IsValid)
             {
                 var articleForCreate = new Article()
                 {
                     Id = Guid.NewGuid(),
                     Title = articleModel.Title,
                     ArticleSourceId = articleModel.SourceId,
                     Date = DateTime.Now,
                     Description = articleModel.Description,
                     Text = articleModel.Description
                 };
                 
                 await _unitOfWork.ArticleRepository.InsertOne(articleForCreate);
                 await _unitOfWork.Commit();

                 return RedirectToAction("Index");
             }
             else
             {
                 var availableSources = await _unitOfWork.ArticleSourceRepository
                     .GetAsQueryable()
                     .AsNoTracking()
                     .Select(source
                         => new SelectListItem(source.Name, source.Id.ToString("D")))
                     .ToListAsync();

                 articleModel.AvailableSources = availableSources;
               
                 return View(articleModel);
             }
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
                        Text = article.Text
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

        [HttpGet]
        public async Task<IActionResult> GetArticles(Guid articleSourceId)
        {
            var articleSource = await _unitOfWork.ArticleSourceRepository.GetById(articleSourceId);
            var model = new ArticleSourceAggregationModel()
            {
                Id = articleSourceId,
                Name = articleSource.Name
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Aggregate(ArticleSourceAggregationModel model)
        {
            var data = await _articleService.AggregateDataFromRssByArticleSourceId(model.Id);

            var existedArticles = await _articleService.GetExistedArticlesUrls();

            var uniqueArticles = data
                .Where(dto => !existedArticles
                    .Any(url => dto.SourceUrl.Equals(url))).ToArray();
            var listFulfilledArticles = new List<ArticleDto>();

            foreach (var articleDto in uniqueArticles)
            {
                var fulFilledArticle = await _articleService.GetArticleByUrl(articleDto.SourceUrl, articleDto);
                if (fulFilledArticle != null)
                {
                    listFulfilledArticles.Add(fulFilledArticle);
                }
            }

            await _articleService.InsertParsedArticles(listFulfilledArticles);
            return RedirectToAction("Index");
        }
        //[NonAction]
        private IActionResult Do()
        {
            return Ok(321);
        }
    }
}
