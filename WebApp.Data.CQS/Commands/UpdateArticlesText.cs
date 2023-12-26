using MediatR;

namespace WebApp.Data.CQS.Commands
{
    public class UpdateArticlesText: IRequest /*<Guid>*/
    {
        public Dictionary<Guid,string> ArticlesData { get; set; }
    }
}
