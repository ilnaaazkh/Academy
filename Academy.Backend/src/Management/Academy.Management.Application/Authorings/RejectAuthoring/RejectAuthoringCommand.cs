using Academy.Core.Abstractions;

namespace Academy.Management.Application.Authorings.RejectAuthoring
{
    public record RejectAuthoringCommand(Guid AuthoringId, string Reason) : ICommand;
}
