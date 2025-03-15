using Academy.Core.Abstractions;
using Academy.CourseManagement.Application.Authorships;
using Academy.SharedKernel.ValueObjects.Ids;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using Academy.Core.Extensions;

namespace Academy.CourseManagement.Application.Courses.Delete
{
    public class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IAuthorshipsRepository _authorshipsRepository;

        public DeleteCourseCommandHandler(
            ICoursesRepository coursesRepository,
            IAuthorshipsRepository authorshipsRepository)
        {
            _coursesRepository = coursesRepository;
            _authorshipsRepository = authorshipsRepository;
        }

        public async Task<UnitResult<ErrorList>> Handle(DeleteCourseCommand command, CancellationToken cancellationToken = default)
        {
            var courseId = CourseId.Create(command.CourseId);
            var course = await _coursesRepository.GetById(courseId, cancellationToken);
            if (course.IsFailure)
            {
                return course.Error.ToErrorList();
            }

            var result = await _coursesRepository.Remove(course.Value, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToErrorList();
            }

            return UnitResult.Success<ErrorList>();
        }
    }

}
