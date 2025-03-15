using Academy.CourseManagement.Application.Authorships;
using Academy.CourseManagement.Domain;
using Academy.CourseManagement.Infrastructure.DbContexts;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Infrastructure.Repositories
{
    internal class AuthorshipsRepository : IAuthorshipsRepository
    {
        private readonly CourseManagementWriteDbContext _dbContext;

        public AuthorshipsRepository(CourseManagementWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UnitResult<Error>> Add(
            Authorship authorship,
            CancellationToken cancellationToken)
        {
            await _dbContext.Authorships.AddAsync(authorship, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return UnitResult.Success<Error>();
        }
    }
}
