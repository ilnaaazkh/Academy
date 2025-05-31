using Academy.Management.Application.Authorings.Command.CreateAuthoring;
using Academy.Management.Application.Authorings.Command.RejectAuthoring;
using Academy.Management.Application.Authorings.Command.UpdateAuthoring;
using Academy.Management.Application.Authorings.Query.GetAuthoringsQuery;
using Academy.Management.Contracts.Requests;

namespace Academy.Management.Presentation.Extensions
{
    public static class CommandExtensions 
    {
        public static GetAuthoringsQuery ToQuery(this GetAuthoringsRequest r) 
            => new(r.PageNumber, r.PageSize);
        public static CreteAuthoringCommand ToCommand(this CreateAuthoringRequest r, Guid userId)
            => new(userId, r.Comment);

        public static RejectAuthoringCommand ToCommand(this RejectAuthoringRequest r, Guid authoringId)
            => new(authoringId, r.Reason);

        public static UpdateAuthoringCommand ToCommad(this UpdateAuthoringRequest r, Guid authoringId, Guid userId)
            => new(authoringId, userId, r.Comment, r.FirstName, r.LastName, r.MiddleName);
    }
}
