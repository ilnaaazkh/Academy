using Academy.Core.Abstractions;
using Academy.SharedKernel.ValueObjects.Ids;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using Academy.Core.Extensions;
using Academy.CourseManagement.Domain;

namespace Academy.CourseManagement.Application.Courses.AddModule
{
    public class AddModuleCommandHandler : ICommandHandler<Guid, AddModuleCommand>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IValidator<AddModuleCommand> _validator;

        public AddModuleCommandHandler(
            ICoursesRepository coursesRepository,
            IValidator<AddModuleCommand> validator)
        {
            _coursesRepository = coursesRepository;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(AddModuleCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
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

            var moduleId = ModuleId.NewModuleId();
            var title = Title.Create(command.Title).Value;
            var description = Description.Create(command.Description).Value;
            var module = new Module(moduleId, title, description);

            course.AddModule(module);

            var saveResult = await _coursesRepository.Save(course, cancellationToken);

            if (saveResult.IsFailure)
            {
                return saveResult.Error.ToErrorList();
            }

            return moduleId.Value;
        }
    }

}
