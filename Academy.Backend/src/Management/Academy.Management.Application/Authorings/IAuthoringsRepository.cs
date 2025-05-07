using Academy.Core.Models;
using Academy.Management.Application.Authorings.Query.GetAuthoringsQuery;
using Academy.Management.Domain;

namespace Academy.Management.Application.Authorings
{
    public interface IAuthoringsRepository
    {
        Task<Guid> Add(Authoring authorRole, CancellationToken cancellationToken);
        Task<(List<Authoring>, int)> GetAuthoringsOnPending(GetAuthoringsQuery query, CancellationToken cancellationToken);
        Task<Authoring?> GetById(Guid authoringId, CancellationToken cancellationToken);
        Task Save(Authoring authorRoleRequest, CancellationToken cancellationToken);
    }
}
