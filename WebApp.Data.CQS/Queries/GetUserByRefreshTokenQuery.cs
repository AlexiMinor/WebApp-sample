using MediatR;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Queries;

public class GetUserByRefreshTokenQuery : IRequest<User>
{
    public Guid RefreshTokenId { get; set; }
}