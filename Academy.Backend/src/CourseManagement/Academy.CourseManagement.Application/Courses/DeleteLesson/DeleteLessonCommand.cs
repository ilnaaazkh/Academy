using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Courses.DeleteLesson
{
    public class DeleteLessonCommandHandler : ICommandHandler<DeleteLessonCommand>
    {
        private readonly ICoursesRepository _coursesRepository;

        public DeleteLessonCommandHandler(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
        public async Task<UnitResult<ErrorList>> Handle(DeleteLessonCommand command, CancellationToken cancellationToken = default)
        {
            var courseId = CourseId.Create(command.CourseId);
            var courseResult = await _coursesRepository.GetById(courseId, cancellationToken);

            if (courseResult.IsFailure)
                return courseResult.Error.ToErrorList();

            var course = courseResult.Value;

            var moduleId = ModuleId.Create(command.ModuleId);
            var lessonId = LessonId.Create(command.LessonId);

            var result = course.RemoveLesson(moduleId, lessonId);

            if(result.IsFailure)
                return result.Error.ToErrorList();

            await _coursesRepository.Save(course, cancellationToken);

            return UnitResult.Success<ErrorList>();
        }
    }

    public record DeleteLessonCommand(
        Guid CourseId,
        Guid ModuleId,
        Guid LessonId) : ICommand;
}
