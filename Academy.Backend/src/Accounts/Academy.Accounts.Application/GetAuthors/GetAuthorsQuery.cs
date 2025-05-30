using Academy.Core.Abstractions;

namespace Academy.Accounts.Application.GetAuthors
{
    public record GetAuthorsQuery(string? SearchQuery) : IQuery;
}
