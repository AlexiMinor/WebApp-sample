using MediatR;
using WebApp.Core;

namespace WebApp.Data.CQS.Commands
{
    public class AddArticleCommand : IRequest/*<Guid>*/
    {
        public ArticleDto Article { get; set; }
    }

    public class DeleteArticleByIdCommand
    {
        public Guid ArticleId { get; set; }
    }
}
