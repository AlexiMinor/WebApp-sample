using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Commands;

namespace WebApp.Data.CQS.CommandHandlers;

public class SetArticleRateCommandHandler : IRequestHandler<SetArticleRateCommand>
{
    private readonly ArticlesAggregatorDbContext _dbContext;

    public SetArticleRateCommandHandler(ArticlesAggregatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(SetArticleRateCommand request, CancellationToken cancellationToken)
    {
        var article = await _dbContext.Articles.FirstOrDefaultAsync(art => art.Id.Equals(request.Id), cancellationToken);
        article.Rate = request.Rate;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}