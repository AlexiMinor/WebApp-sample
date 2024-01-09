using MediatR;
using WebApp.Core;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Queries;

public class GetUserByEmailQuery : IRequest<User>//articleDto
{
   public string Email { get; set; }
}