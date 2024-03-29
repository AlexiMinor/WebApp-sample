﻿using MediatR;
using WebApp.Data.CQS.Commands;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.CommandHandlers
{
    
    public class AddRefreshTokenCommandHandler : IRequestHandler<AddRefreshTokenCommand, Guid>
    {
        private readonly ArticlesAggregatorDbContext _dbContext;


        public AddRefreshTokenCommandHandler(ArticlesAggregatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(AddRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var rt = new RefreshToken()
                {
                    Id = Guid.NewGuid(),
                    GeneratedAt = DateTime.UtcNow,
                    ExpiringAt = DateTime.UtcNow.AddHours(1),//should be in config
                    AssociatedDeviceName = command.Ip,
                    UserId = command.UserId

                };
                await _dbContext.RefreshTokens.AddAsync(rt, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return rt.Id;
            }
            catch (Exception e)
            {
                //log
                return Guid.Empty;
            }
        }
    }

}
