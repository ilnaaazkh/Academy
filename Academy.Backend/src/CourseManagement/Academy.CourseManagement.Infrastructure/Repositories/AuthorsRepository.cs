using Academy.CourseManagement.Application.Authors;
using Academy.CourseManagement.Domain;
using Academy.CourseManagement.Infrastructure.DbContexts;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Academy.CourseManagement.Infrastructure.Repositories
{
    internal class AuthorsRepository : IAuthorsRepository
    {
        private readonly CourseManagementWriteDbContext _dbContext;

        public AuthorsRepository(CourseManagementWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(Author author, 
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Authors.AddAsync(author, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return author.Id;
        } 

        public async Task<Result<Guid, Error>> Remove(Author author, 
            CancellationToken cancellationToken = default)
        {
            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return author.Id.Value;
        }

        public async Task<Result<Author, Error>> GetById(AuthorId id, 
            CancellationToken cancellationToken = default)
        {
            var author =  await _dbContext.Authors
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

            if(author is null)
            {
                return Errors.General.NotFound(id.Value);
            }

            return author;
        }

        public async Task<Result<Guid, Error>> Save(Author author, 
            CancellationToken cancellationToken = default)
        {
            _dbContext.Authors.Attach(author);
            await _dbContext.SaveChangesAsync();

            return author.Id.Value;
        }

        public async Task<bool> IsAuthorExist(AuthorId id)
        {
            return await _dbContext.Authors.AnyAsync(a => a.Id == id);
        }
    }
}
