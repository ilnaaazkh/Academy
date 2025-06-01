using Academy.Core.Models;
using Academy.Management.Application.Authorings;
using Academy.Management.Application.Authorings.Query.GetAuthoringsQuery;
using Academy.Management.Domain;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.Marshalling;

namespace Academy.Management.Infrastructure.Repositories
{
    internal class AuthoringsRepository : IAuthoringsRepository
    {
        private readonly ManagementDbContext _dbContext;

        public AuthoringsRepository(ManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(Authoring authorRole, CancellationToken cancellationToken)
        {
            await _dbContext.Authorings.AddAsync(authorRole, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return authorRole.Id;
        }

        public async Task<Authoring?> GetById(Guid authoringId, CancellationToken cancellationToken)
        {
            return await _dbContext.Authorings.FirstOrDefaultAsync(a => a.Id == authoringId, cancellationToken);
        }

        public async Task Save(Authoring authorRoleRequest, CancellationToken cancellationToken)
        {
            _dbContext.Authorings.Attach(authorRoleRequest);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<(List<Authoring>, int)> GetAuthoringsOnPending(GetAuthoringsQuery query, CancellationToken cancellationToken)
        {
            var authoringsQueryable = _dbContext.Authorings
                                    .Where(a => a.Status == Domain.AuthorRoleRequestStatus.Pending)
                                    .OrderByDescending(a => a.CreatedAt);
            
            var totalCount = authoringsQueryable.Count();
            var result = await authoringsQueryable.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync(); 

            return (result, totalCount);
        }

        public async Task<IReadOnlyList<Authoring>> GetAuthoringsCreatedByUser(Guid userId, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Authorings
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
