using Academy.Core.Abstractions;
using Academy.SharedKernel.ValueObjects.Ids;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Academy.Core.Extensions;

namespace Academy.CourseManagement.Application.Courses.HideCourse
{
    public class HideCourseCommandHandler : ICommandHandler<Guid, HideCourseCommand>
    {
        private readonly ICoursesRepository _coursesRepository;

        public HideCourseCommandHandler(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<Result<Guid, ErrorList>> Handle(HideCourseCommand command, CancellationToken cancellationToken = default)
        {
            var courseId = CourseId.Create(command.CourseId);
            var courseResult = await _coursesRepository.GetById(courseId, cancellationToken);

            if (courseResult.IsFailure)
                return courseResult.Error.ToErrorList();

            var course = courseResult.Value;

            if (course.AuthorId != command.UserId)
                return Errors.User.AccessDenied().ToErrorList();

            course.Hide();

            await _coursesRepository.Save(course, cancellationToken);

            return course.Id.Value;
        }
    }
}
