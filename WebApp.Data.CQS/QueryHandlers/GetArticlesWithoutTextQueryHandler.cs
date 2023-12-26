using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetArticlesWithoutTextQueryHandler : IRequestHandler<GetArticlesWithoutTextQuery,
    List<(Guid, string)>>
{
    private readonly ArticlesAggregatorDbContext _dbContext;

    public GetArticlesWithoutTextQueryHandler(ArticlesAggregatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<(Guid, string)>> Handle(GetArticlesWithoutTextQuery request, 
        CancellationToken cancellationToken)
    {
        var article = await _dbContext.Articles
            .Where(art => string.IsNullOrEmpty(art.Text))
            .Select(art => new { art.Id, art.SourceUrl })
            .ToListAsync(cancellationToken);

        var resultList = article.Select(arg => (arg.Id, arg.SourceUrl)).ToList();

        return resultList;
    }
}