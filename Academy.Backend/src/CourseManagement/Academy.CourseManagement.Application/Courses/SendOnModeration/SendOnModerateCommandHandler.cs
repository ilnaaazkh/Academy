using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Courses.SendOnModeration
{
    public class SendOnModerateCommandHandler : ICommandHandler<Guid, SendOnModerationCommand>
    {
        private readonly ICoursesRepository _coursesRepository;

        public SendOnModerateCommandHandler(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<Result<Guid, ErrorList>> Handle(SendOnModerationCommand command, CancellationToken cancellationToken = default)
        {
            var courseId = CourseId.Create(command.CourseId);
            var courseResult = await _coursesRepository.GetById(courseId, cancellationToken);

            if (courseResult.IsFailure)
                return courseResult.Error.ToErrorList();

            var course = courseResult.Value;

            if (course.AuthorId != command.UserId)
                return Errors.User.AccessDenied().ToErrorList();

            course.SendOnReview();

            await _coursesRepository.Save(course, cancellationToken);

            return course.Id.Value;
        }
    }
}
