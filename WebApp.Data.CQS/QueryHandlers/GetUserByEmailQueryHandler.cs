using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User>
{
    private readonly ArticlesAggregatorDbContext _dbContext;

    public GetUserByEmailQueryHandler(ArticlesAggregatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Handle(GetUserByEmailQuery request, 
        CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(article1 => article1.Email.Equals(request.Email),
                cancellationToken: cancellationToken);
        return user;
    }
}