using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetArticleTextQueryHandler : IRequestHandler<GetArticleTextQuery, string>
{
    private readonly ArticlesAggregatorDbContext _dbContext;

    public GetArticleTextQueryHandler(ArticlesAggregatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(GetArticleTextQuery request,
        CancellationToken cancellationToken)
    {
        var article = await _dbContext.Articles
            .FirstOrDefaultAsync(article1 => article1.Id.Equals(request.Id),
                cancellationToken: cancellationToken);

        return article?.Text;

        //convert to DTO

        //if article null
    }
}