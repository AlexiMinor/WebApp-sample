using MediatR;
using WebApp.Core;
using WebApp.Data.CQS.Commands;
using WebApp.Mappers;

namespace WebApp.Data.CQS.CommandHandlers
{
    
    public class AddArticleCommandHandler : IRequestHandler<AddArticleCommand/*, Guid*/>
    {
        private readonly ArticlesAggregatorDbContext _dbContext;
        private readonly ArticleMapper _mapper;

        public AddArticleCommandHandler(ArticlesAggregatorDbContext dbContext, 
            ArticleMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task/*<Guid>*/ Handle(AddArticleCommand command, CancellationToken cancellationToken)
        {
            var article = _mapper.ArticleDtoToArticle(command.Article);
            var entry = await _dbContext.Articles.AddAsync(article, cancellationToken);
            //var id = entry.Entity.Id;//if int  - you will get the value
            await _dbContext.SaveChangesAsync(cancellationToken);

            //return id;
        }
    }

}
