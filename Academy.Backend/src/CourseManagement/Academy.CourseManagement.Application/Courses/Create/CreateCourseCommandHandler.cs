using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;
using Academy.CourseManagement.Domain;
using Academy.CourseManagement.Application.Authors;
using Academy.CourseManagement.Application.Authorships;

namespace Academy.CourseManagement.Application.Courses.Create
{
    public class CreateCourseCommandHandler : ICommandHandler<Guid, CreateCourseCommand>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IAuthorshipsRepository _authorshipsRepository;
        private readonly IValidator<CreateCourseCommand> _validator;

        public CreateCourseCommandHandler(
            ICoursesRepository coursesRepository,
            IAuthorsRepository authorsRepository,
            IAuthorshipsRepository authorshipsRepository,
            IValidator<CreateCourseCommand> validator)
        {
            _coursesRepository = coursesRepository;
            _authorsRepository = authorsRepository;
            _authorshipsRepository = authorshipsRepository;
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

            var authorId = AuthorId.Create(command.AuthorId);
            bool isAuthorExist = await _authorsRepository.IsAuthorExist(authorId);

            if (isAuthorExist == false)
            {
                return Errors.Author.AuthorNotExist(authorId.Value).ToErrorList();
            }

            var courseId = CourseId.NewCourseId();
            var title = Title.Create(command.Title).Value;
            var description = Description.Create(command.Description).Value;

            var course = new Course(courseId, title, description);
            var guid = await _coursesRepository.Add(course, cancellationToken);

            var authorship = new Authorship(courseId, authorId);
            await _authorshipsRepository.Add(authorship, cancellationToken);

            return guid;
        }
    }
}
