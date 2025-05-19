using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.AddLessonContent
{
    public class AddLessonContentCommandHandler : ICommandHandler<Guid, AddLessonContentCommand>
    {
        private readonly ICoursesRepository _coursesRepository;

        public AddLessonContentCommandHandler(
            ICoursesRepository coursesRepository,
            IValidator<AddLessonContentCommand> validator)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<Result<Guid, ErrorList>> Handle(AddLessonContentCommand command, CancellationToken cancellationToken = default)
        {
            var courseId = CourseId.Create(command.CourseId);
            var courseResult = await _coursesRepository.GetById(courseId, cancellationToken);

            if (courseResult.IsFailure)
                return courseResult.Error.ToErrorList();
            

            if (courseResult.Value.AuthorId != command.UserId)
                return Errors.User.AccessDenied().ToErrorList();

            var moduleId = ModuleId.Create(command.ModuleId);
            var lessonId = LessonId.Create(command.LessonId);
            var content = Content.Create(command.Content).Value;

            var result = courseResult.Value.SetLessonContent(moduleId, lessonId, content);

            if(result.IsFailure)
                return result.Error.ToErrorList();

            await _coursesRepository.Save(courseResult.Value, cancellationToken);

            return courseResult.Value.Id.Value;
        }
    }
}
