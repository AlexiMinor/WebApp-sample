using System.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Core;
using WebApp.Data.CQS.Commands;
using WebApp.Data.Entities;
using WebApp.Mappers;

namespace WebApp.Data.CQS.CommandHandlers
{
    //synthetic sample
    public class AddArticleWithSourceCommandHandler : IRequestHandler<AddArticleWithSourceCommand>
    {
        private readonly ArticlesAggregatorDbContext _dbContext;

        public AddArticleWithSourceCommandHandler(ArticlesAggregatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(AddArticleWithSourceCommand request,
            CancellationToken cancellationToken)
        {
            var isSourceExists = await _dbContext.ArticleSources
                .AnyAsync(source => source.Id.Equals(request.ArticleSourceId), cancellationToken);

            if (isSourceExists)
            {
                var article = new Article()
                {
                    Id = Guid.NewGuid(),
                    ArticleSourceId = request.ArticleSourceId,
                    //...
                };
                await _dbContext.Articles.AddAsync(article, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            else
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
                {
                    var source = new ArticleSource()
                    {
                        Id = request.ArticleSourceId,
                        Name = request.SourceName,
                        RssUrl = request.SourceRssUrl,
                        Url = request.SourceBaseUrl
                    };
                    await _dbContext.ArticleSources.AddAsync(source,cancellationToken);
                    //await _dbContext.SaveChangesAsync(cancellationToken);

                    var article = new Article()
                    {
                        Id = Guid.NewGuid(),
                        ArticleSourceId = request.ArticleSourceId,
                    };

                    await _dbContext.Articles.AddAsync(article, cancellationToken);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);
                }
            }
        }
    }

}
