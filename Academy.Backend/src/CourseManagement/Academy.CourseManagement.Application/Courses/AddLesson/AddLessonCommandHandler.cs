using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.CourseManagement.Domain;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.AddLesson
{
    public class AddLessonCommandHandler : ICommandHandler<Guid, AddLessonCommand>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IValidator<AddLessonCommand> _validator;

        public AddLessonCommandHandler(
            ICoursesRepository coursesRepository,
            IValidator<AddLessonCommand> validator)
        {
            _coursesRepository = coursesRepository;
            _validator = validator;
        }
        public async Task<Result<Guid, ErrorList>> Handle(AddLessonCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if(validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var courseId = CourseId.Create(command.CourseId);
            var courseResult = await _coursesRepository.GetById(courseId, cancellationToken);

            if (courseResult.IsFailure)
            {
                return courseResult.Error.ToErrorList();
            }

            var course = courseResult.Value;

            if (course.AuthorId != command.UserId)
                return Errors.User.AccessDenied().ToErrorList();

            var moduleId = ModuleId.Create(command.ModuleId);
            var lessonId = LessonId.NewLessonId();
            var title = Title.Create(command.Title).Value;
            var content = Content.Create(command.Content).Value;
            var lessonType = LessonType.Create(command.LessonType).Value;

            var lesson = new Lesson(lessonId, title, content, lessonType);

            var result = course.AddLesson(moduleId, lesson);

            if (result.IsFailure)
            {
                return result.Error.ToErrorList();
            }

            await _coursesRepository.Save(course, cancellationToken);

            return lessonId.Value;
        }
    }
}
