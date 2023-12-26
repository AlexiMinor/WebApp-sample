using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetUnratedArticleIdsQueryHandler : IRequestHandler<GetUnratedArticleIdsQuery, Guid[]>
{
    private readonly ArticlesAggregatorDbContext _dbContext;

    public GetUnratedArticleIdsQueryHandler(ArticlesAggregatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid[]> Handle(GetUnratedArticleIdsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Articles
            .AsNoTracking()
            .Where(article => article.Rate == null)
            .Take(request.MaxTake)
            .Select(article => article.Id)
            .ToArrayAsync(cancellationToken: cancellationToken);

    }
}