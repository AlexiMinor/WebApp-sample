using MediatR;
using WebApp.Core;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Queries;

public class GetArticleTextQuery : IRequest<string>
{
   public Guid Id { get; set; }
}