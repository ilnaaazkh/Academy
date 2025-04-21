using Academy.Core.Abstractions;

namespace Academy.Management.Application.Authorings.Command.SubmitAuthoring
{
    public record SubmitAuthoringCommand(Guid AuthoringId, Guid UserId) : ICommand;
}
