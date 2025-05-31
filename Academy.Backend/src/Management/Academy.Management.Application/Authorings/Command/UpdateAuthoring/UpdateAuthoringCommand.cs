using Academy.Core.Abstractions;

namespace Academy.Management.Application.Authorings.Command.UpdateAuthoring
{
    public record UpdateAuthoringCommand(
        Guid AuthoringId, 
        Guid UserId, 
        string Comment,
        string FirstName, 
        string LastName, 
        string MiddleName) : ICommand;
}
