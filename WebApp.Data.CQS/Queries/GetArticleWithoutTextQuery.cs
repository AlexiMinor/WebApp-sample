using MediatR;

namespace WebApp.Data.CQS.Queries;

public class GetArticlesWithoutTextQuery  : IRequest<List<(Guid, string)>>
{
    //Guid SourceId;
}