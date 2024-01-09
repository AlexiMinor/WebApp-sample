using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Commands;

namespace WebApp.Data.CQS.CommandHandlers
{

    public class DeleteRefreshTokenCommandHandler : IRequestHandler<DeleteRefreshTokenCommand>
    {
        private readonly ArticlesAggregatorDbContext _dbContext;


        public DeleteRefreshTokenCommandHandler(ArticlesAggregatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var rt = await _dbContext.RefreshTokens.FirstOrDefaultAsync(token =>
                token.Id.Equals(command.Id), cancellationToken);
            _dbContext.RefreshTokens.Remove(rt);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
