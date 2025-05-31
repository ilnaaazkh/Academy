using Academy.Core.Abstractions;
using Academy.SharedKernel.ValueObjects.Ids;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Academy.Core.Extensions;

namespace Academy.CourseManagement.Application.Courses.Publish
{
    public class PublishCommandHandler : ICommandHandler<Guid, PublishCommand>
    {
        private readonly ICoursesRepository _coursesRepository;

        public PublishCommandHandler(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<Result<Guid, ErrorList>> Handle(PublishCommand command, CancellationToken cancellationToken = default)
        {
            var courseId = CourseId.Create(command.CourseId);
            var courseResult = await _coursesRepository.GetById(courseId, cancellationToken);

            if (courseResult.IsFailure)
                return courseResult.Error.ToErrorList();

            var course = courseResult.Value;

            course.Publish();

            await _coursesRepository.Save(course, cancellationToken);

            return course.Id.Value;
        }
    }
}
