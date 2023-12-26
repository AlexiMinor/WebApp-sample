﻿using MediatR;
using WebApp.Data.CQS.Commands;
using WebApp.Mappers;

namespace WebApp.Data.CQS.CommandHandlers
{
    
    public class InsertRssDataCommandHandler : IRequestHandler<InsertRssDataCommand/*, Guid*/>
    {
        private readonly ArticlesAggregatorDbContext _dbContext;
        private readonly ArticleMapper _mapper;

        public InsertRssDataCommandHandler(ArticlesAggregatorDbContext dbContext, 
            ArticleMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(InsertRssDataCommand command, CancellationToken cancellationToken)
        {
            var articles = command.Articles
                .Select(dto => _mapper.ArticleDtoToArticle(dto))
                .ToArray();
            await _dbContext.Articles.AddRangeAsync(articles, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

        }
    }

}
