using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.AddTestToLesson
{
    public class AddTestToLessonCommandHandler : ICommandHandler<Guid, AddTestToLessonCommand>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IValidator<AddTestToLessonCommand> _validator;

        public AddTestToLessonCommandHandler(
            ICoursesRepository coursesRepository,
            IValidator<AddTestToLessonCommand> validator)
        {
            _coursesRepository = coursesRepository;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(AddTestToLessonCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            var courseId = CourseId.Create(command.CourseId);
            var courseResult = await _coursesRepository.GetById(courseId, cancellationToken);

            if (courseResult.IsFailure)
                return courseResult.Error.ToErrorList();

            var course = courseResult.Value;
            var moduleId = ModuleId.Create(command.ModuleId);
            var lessonId = LessonId.Create(command.LessonId);

            var questions = command.Questions
                                    .Select(q =>
                                    {
                                        var answers = q.Answers
                                            .Select(a => Answer.Create(a.Title, a.IsCorrect).Value)
                                            .ToList();

                                        return Question.Create(q.Title, answers).Value;
                                    });

            var addTestResult = course.AddTestToLesson(moduleId, lessonId, questions);

            if(addTestResult.IsFailure)
                return addTestResult.Error.ToErrorList();

            await _coursesRepository.Save(course, cancellationToken);

            return lessonId.Value;
        }
    }
}
