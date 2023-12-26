using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Commands;
using WebApp.Mappers;

namespace WebApp.Data.CQS.CommandHandlers
{
    public class UpdateArticlesTextHandler : IRequestHandler<UpdateArticlesText/*, Guid*/>
    {
        private readonly ArticlesAggregatorDbContext _dbContext;
        private readonly ArticleMapper _mapper;

        public UpdateArticlesTextHandler(ArticlesAggregatorDbContext dbContext, 
            ArticleMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(UpdateArticlesText command, CancellationToken cancellationToken)
        {
            var articles = await _dbContext.Articles.Where(article => command.ArticlesData.Keys
                .Contains(article.Id))
                .ToListAsync(cancellationToken);

            foreach (var article in articles)
            {
                article.Text = command.ArticlesData[article.Id];
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

        }
    }

}
