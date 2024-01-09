using MediatR;

namespace WebApp.Data.CQS.Commands;

public class AddRefreshTokenCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Ip { get; set; }
}