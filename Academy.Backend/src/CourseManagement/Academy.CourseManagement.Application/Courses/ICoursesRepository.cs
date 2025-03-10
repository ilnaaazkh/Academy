using Academy.CourseManagement.Domain;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Courses
{
    public interface ICoursesRepository
    {
        Task<Guid> Add(Course course, CancellationToken cancellationToken = default);
        Task<Result<Course, Error>> GetById(CourseId id, CancellationToken cancellationToken = default);
        Task<Result<Guid, Error>> Remove(Course course, CancellationToken cancellationToken = default);
        Task<Result<Guid, Error>> Save(Course course, CancellationToken cancellationToken = default);
    }
}
