using Academy.Management.Application.Authorings;
using Academy.Management.Domain;
using Microsoft.EntityFrameworkCore;

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
    }
}
