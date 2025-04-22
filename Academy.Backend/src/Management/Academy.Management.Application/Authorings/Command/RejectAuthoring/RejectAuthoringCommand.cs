using Academy.Core.Abstractions;

namespace Academy.Management.Application.Authorings.Command.RejectAuthoring
{
    public record RejectAuthoringCommand(Guid AuthoringId, string Reason) : ICommand;
}
