using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.MVC7.Models;
using WebApp.Repositories;

namespace WebApp.MVC7.Controllers
{
    public class ArticleSourceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleSourceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> List()
        {
            var articleSources = await _unitOfWork
                .ArticleSourceRepository.GetAsQueryable().ToListAsync();

            var model = articleSources.Select(source => new ArticleSourceModel()
            {
                Id = source.Id,
                Name = source.Name,
                Url = source.Url,
                RssUrl = source.RssUrl
            }).ToList();

            return View(model);
        }
    }
}
