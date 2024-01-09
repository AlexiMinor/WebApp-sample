using MediatR;
using WebApp.Core;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Queries;

public class GetRefreshTokenQuery : IRequest<RefreshToken>
{
   public Guid Id { get; set; }
}