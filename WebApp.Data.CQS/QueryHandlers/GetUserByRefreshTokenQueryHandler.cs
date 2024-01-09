using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetUserByRefreshTokenQueryHandler : IRequestHandler<GetUserByRefreshTokenQuery, User>
{
    private readonly ArticlesAggregatorDbContext _articlesAggregatorDbContext;

    public GetUserByRefreshTokenQueryHandler(ArticlesAggregatorDbContext articlesAggregatorDbContext)
    {
        _articlesAggregatorDbContext = articlesAggregatorDbContext;
    }

    public async Task<User> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var refreshToken = await _articlesAggregatorDbContext.RefreshTokens
            .FirstOrDefaultAsync(rt=> rt.Id.Equals(request.RefreshTokenId),
                cancellationToken: cancellationToken);

        if (refreshToken == null)
        {
            return null;
        }

        var user = await _articlesAggregatorDbContext.Users
            .FirstOrDefaultAsync(u => u.Id.Equals(refreshToken.UserId),
                cancellationToken: cancellationToken);
        return user;
    }
}