using Academy.CourseManagement.Domain;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Authors
{
    public interface IAuthorsRepository
    {
        Task<Guid> Add(Author author, CancellationToken cancellationToken = default);
        Task<Result<Author, Error>> GetById(AuthorId id, CancellationToken cancellationToken = default);
        Task<Result<Guid, Error>> Remove(Author author, CancellationToken cancellationToken = default);
        Task<Result<Guid, Error>> Save(Author author, CancellationToken cancellationToken = default);
        Task<bool> IsAuthorExist(AuthorId id);
    }
}
