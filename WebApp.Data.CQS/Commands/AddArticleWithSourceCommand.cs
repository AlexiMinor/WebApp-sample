using MediatR;
using WebApp.Core;

namespace WebApp.Data.CQS.Commands
{
    public class AddArticleWithSourceCommand : IRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string SourceUrl { get; set; }
        public Guid ArticleSourceId { get; set; }

        public string SourceName { get; set; }

        public string SourceBaseUrl { get; set; }

        public string SourceRssUrl { get; set; }
    }
}
