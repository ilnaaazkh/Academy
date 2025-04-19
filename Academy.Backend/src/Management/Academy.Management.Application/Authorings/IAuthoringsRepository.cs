using Academy.Management.Domain;

namespace Academy.Management.Application.Authorings
{
    public interface IAuthoringsRepository
    {
        Task<Guid> Add(Authoring authorRole, CancellationToken cancellationToken);
        Task<Authoring> GetById(Guid authoringId, CancellationToken cancellationToken);
        Task Save(Authoring authorRoleRequest, CancellationToken cancellationToken);
    }
}
