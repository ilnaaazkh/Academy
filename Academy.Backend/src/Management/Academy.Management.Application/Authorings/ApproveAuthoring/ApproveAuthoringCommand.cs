using Academy.Core.Abstractions;

namespace Academy.Management.Application.Authorings.ApproveAuthoring
{
    public record ApproveAuthoringCommand(Guid AuthoringId) : ICommand;
}
