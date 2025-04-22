using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.UpdateModule
{
    public class UpdateModuleCommandHandler : ICommandHandler<Guid, UpdateModuleCommand>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IValidator<UpdateModuleCommand> _validator;

        public UpdateModuleCommandHandler(
            ICoursesRepository coursesRepository,
            IValidator<UpdateModuleCommand> validator)
        {
            _coursesRepository = coursesRepository;
            _validator = validator;
        }
        public async Task<Result<Guid, ErrorList>> Handle(
            UpdateModuleCommand command, 
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();
            

            var courseId = CourseId.Create(command.CourseId);
            var courseResult = await _coursesRepository.GetById(courseId, cancellationToken);

            if (courseResult.IsFailure)
                return courseResult.Error.ToErrorList();

            var course = courseResult.Value;

            if (course.AuthorId != command.UserId)
                return Errors.User.AccessDenied().ToErrorList();

            var moduleId = ModuleId.Create(command.ModuleId);
            var moduleResult = course.GetModuleById(moduleId);

            if (moduleResult.IsFailure)
            {
                return moduleResult.Error.ToErrorList();
            }

            var module = moduleResult.Value;
            Title title = Title.Create(command.Title).Value;
            module.UpdateTitle(title);

            Description description = Description.Create(command.Description).Value;
            module.UpdateDescription(description);

            var saveResult = await _coursesRepository.Save(course, cancellationToken);

            if(saveResult.IsFailure)
                return saveResult.Error.ToErrorList();

            return moduleId.Value;
        }
    }
}
