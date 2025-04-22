using Academy.Core.Abstractions;
using Academy.SharedKernel.ValueObjects.Ids;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Academy.Core.Extensions;

namespace Academy.CourseManagement.Application.Courses.Delete
{
    public class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand>
    {
        private readonly ICoursesRepository _coursesRepository;

        public DeleteCourseCommandHandler(
            ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<UnitResult<ErrorList>> Handle(DeleteCourseCommand command, CancellationToken cancellationToken = default)
        {
            var courseId = CourseId.Create(command.CourseId);
            var courseResult = await _coursesRepository.GetById(courseId, cancellationToken);
            if (courseResult.IsFailure)
            {
                return courseResult.Error.ToErrorList();
            }

            var course = courseResult.Value;

            if (course.AuthorId != command.UserId)
                return Errors.User.AccessDenied().ToErrorList();

            var result = await _coursesRepository.Remove(course, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToErrorList();
            }

            return UnitResult.Success<ErrorList>();
        }
    }

}
