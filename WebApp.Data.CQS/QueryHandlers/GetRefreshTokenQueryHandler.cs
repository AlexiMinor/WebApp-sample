using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetRefreshTokenQueryHandler : IRequestHandler<GetRefreshTokenQuery, RefreshToken>
{
    private readonly ArticlesAggregatorDbContext _dbContext;

    public GetRefreshTokenQueryHandler(ArticlesAggregatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RefreshToken> Handle(GetRefreshTokenQuery request, 
        CancellationToken cancellationToken)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(article1 => article1.Id.Equals(request.Id),
                cancellationToken: cancellationToken);
        return refreshToken;
    }
}