using Academy.Management.Application.Authorings.CreateAuthoring;
using Academy.Management.Application.Authorings.RejectAuthoring;
using Academy.Management.Contracts.Requests;

namespace Academy.Management.Presentation.Extensions
{
    public static class CommandExtensions 
    {
        public static CreteAuthoringCommand ToCommand(this CreateAuthoringRequest request, Guid userId)
            => new(userId, request.Comment);

        public static RejectAuthoringCommand ToCommand(this RejectAuthoringRequest request, Guid authoringId)
            => new(authoringId, request.Reason);
    }
}
