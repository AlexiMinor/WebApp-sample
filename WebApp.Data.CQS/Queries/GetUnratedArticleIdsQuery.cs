using MediatR;

namespace WebApp.Data.CQS.Queries;

public class GetUnratedArticleIdsQuery : IRequest<Guid[]>
{
    public int MaxTake { get; set; }

    public GetUnratedArticleIdsQuery(int maxTale = 25)
    {
        MaxTake = maxTale;
    }
}