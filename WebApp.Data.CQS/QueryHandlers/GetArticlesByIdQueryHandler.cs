using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetArticlesByIdQueryHandler : IRequestHandler<GetArticlesByIdQuery, Article>
{
    private readonly ArticlesAggregatorDbContext _dbContext;

    public GetArticlesByIdQueryHandler(ArticlesAggregatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Article> Handle(GetArticlesByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var article = await _dbContext.Articles
            .FirstOrDefaultAsync(article1 => article1.Id.Equals(request.Id),
                cancellationToken: cancellationToken);
        //convert to DTO

        //if article null
        return article;
    }
}