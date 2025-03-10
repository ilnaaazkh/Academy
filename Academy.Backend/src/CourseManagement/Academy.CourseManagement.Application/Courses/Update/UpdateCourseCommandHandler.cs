
using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.Update
{
    public class UpdateCourseCommandHandler : ICommandHandler<Guid, UpdateCourseCommand>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IValidator<UpdateCourseCommand> _validator;

        public UpdateCourseCommandHandler(
            ICoursesRepository coursesRepository,
            IValidator<UpdateCourseCommand> validator)
        {
            _coursesRepository = coursesRepository;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            UpdateCourseCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToErrorList();
            }

            var courseId = CourseId.Create(command.Id);
            var existingCourseResult = await _coursesRepository.GetById(courseId, cancellationToken);

            if (existingCourseResult.IsFailure)
            {
                return existingCourseResult.Error.ToErrorList();
            }

            var course = existingCourseResult.Value;
            course.UpdateTitle(Title.Create(command.Title).Value);
            course.UpdateDescription(Description.Create(command.Description).Value);

            var result = await _coursesRepository.Save(course, cancellationToken);

            return result.Value;
        }
    }
}
