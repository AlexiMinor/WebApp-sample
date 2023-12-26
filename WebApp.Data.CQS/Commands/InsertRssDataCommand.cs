using MediatR;
using WebApp.Core;

namespace WebApp.Data.CQS.Commands
{
    public class InsertRssDataCommand : IRequest/*<Guid>*/
    {
        public ArticleDto[] Articles { get; set; }
    }
}
