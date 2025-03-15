using Academy.CourseManagement.Domain;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Authorships
{
    public interface IAuthorshipsRepository
    {
        Task<UnitResult<Error>> Add(Authorship authorship, CancellationToken cancellationToken = default);
    }
}
