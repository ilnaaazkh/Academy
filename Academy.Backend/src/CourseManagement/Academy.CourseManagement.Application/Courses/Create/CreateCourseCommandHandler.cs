using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;
using Academy.CourseManagement.Domain;

namespace Academy.CourseManagement.Application.Courses.Create
{
    public class CreateCourseCommandHandler : ICommandHandler<Guid, CreateCourseCommand>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IValidator<CreateCourseCommand> _validator;

        public CreateCourseCommandHandler(
            ICoursesRepository coursesRepository,
            IValidator<CreateCourseCommand> validator)
        {
            _coursesRepository = coursesRepository;
            _validator = validator;
        }
        public async Task<Result<Guid, ErrorList>> Handle(
            CreateCourseCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var courseId = CourseId.NewCourseId();
            var title = Title.Create(command.Title).Value;
            var description = Description.Create(command.Description).Value;

            var course = new Course(courseId, title, description, command.AuthorId);
            var guid = await _coursesRepository.Add(course, cancellationToken);

            return guid;
        }
    }
}
