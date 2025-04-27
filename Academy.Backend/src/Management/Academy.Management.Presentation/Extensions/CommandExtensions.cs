using Academy.Management.Application.Authorings.Command.CreateAuthoring;
using Academy.Management.Application.Authorings.Command.RejectAuthoring;
using Academy.Management.Application.Authorings.Query.GetAuthoringsQuery;
using Academy.Management.Contracts.Requests;

namespace Academy.Management.Presentation.Extensions
{
    public static class CommandExtensions 
    {
        public static GetAuthoringsQuery ToQuery(this GetAuthoringsRequest request) 
            => new(request.PageNumber, request.PageSize);
        public static CreteAuthoringCommand ToCommand(this CreateAuthoringRequest request, Guid userId)
            => new(userId, request.Comment);

        public static RejectAuthoringCommand ToCommand(this RejectAuthoringRequest request, Guid authoringId)
            => new(authoringId, request.Reason);
    }
}
