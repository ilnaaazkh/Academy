using Academy.Core.Abstractions;

namespace Academy.Accounts.Application.DeleteAuthor
{
    public record DeleteAuthorCommand(Guid AuthorId) : ICommand;
}
