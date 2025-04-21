using Academy.Core.Abstractions;

namespace Academy.Management.Application.Authorings.Command.ApproveAuthoring
{
    public record ApproveAuthoringCommand(Guid AuthoringId) : ICommand;
}
