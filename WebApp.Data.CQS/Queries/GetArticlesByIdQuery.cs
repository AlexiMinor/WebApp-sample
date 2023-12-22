using MediatR;
using WebApp.Core;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Queries;

public class GetArticlesByIdQuery : IRequest<Article>//articleDto
{
   public Guid Id { get; set; }
}