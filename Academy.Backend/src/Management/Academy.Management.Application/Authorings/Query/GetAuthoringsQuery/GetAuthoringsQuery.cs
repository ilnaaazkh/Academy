using Academy.Core.Abstractions;

namespace Academy.Management.Application.Authorings.Query.GetAuthoringsQuery
{
    public record GetAuthoringsQuery(int PageNumber, int PageSize) : IQuery;
}
