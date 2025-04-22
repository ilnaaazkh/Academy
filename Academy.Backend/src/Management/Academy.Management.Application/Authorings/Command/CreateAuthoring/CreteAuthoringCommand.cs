using Academy.Core.Abstractions;


namespace Academy.Management.Application.Authorings.Command.CreateAuthoring
{
    public record CreteAuthoringCommand(Guid UserId, string Comment) : ICommand;
}
