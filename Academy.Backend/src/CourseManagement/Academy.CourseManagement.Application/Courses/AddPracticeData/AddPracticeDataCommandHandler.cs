using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.AddPracticeData
{
    public class AddPracticeDataCommandHandler : ICommandHandler<Guid, AddPracticeDataCommand>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IValidator<AddPracticeDataCommand> _validator;

        public AddPracticeDataCommandHandler(
            ICoursesRepository coursesRepository,
            IValidator<AddPracticeDataCommand> validator)
        {
            _coursesRepository = coursesRepository;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            AddPracticeDataCommand command, 
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken); 
            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();
            
            var courseResult = await _coursesRepository.GetById(CourseId.Create(command.CourseId));

            if (courseResult.IsFailure)
                return courseResult.Error.ToErrorList();

            var moduleId = ModuleId.Create(command.ModuleId);
            var lessonId = LessonId.Create(command.LessonId);

            var practiceData = PracticeLessonData.Create(
                command.TemplateCode, 
                command.Tests.Select(t => new Test(t.Input.ToList(), t.Expected))).Value;

            var result = courseResult.Value.AddPracticeDataToLesson(moduleId, lessonId, practiceData);

            if (result.IsFailure)
                return result.Error.ToErrorList();

            await _coursesRepository.Save(courseResult.Value, cancellationToken);

            return lessonId.Value;
        }
    }
}
