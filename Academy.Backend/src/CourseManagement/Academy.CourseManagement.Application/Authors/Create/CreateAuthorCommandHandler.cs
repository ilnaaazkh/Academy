using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.CourseManagement.Domain;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Academy.CourseManagement.Application.Authors.Create
{
    public class CreateAuthorCommandHandler : ICommandHandler<Guid, CreateAuthorCommand>
    {
        private readonly IValidator<CreateAuthorCommand> _validator;
        private readonly IAuthorsRepository _authorsRepository;
        public CreateAuthorCommandHandler(
            IAuthorsRepository authorsRepository,
            IValidator<CreateAuthorCommand> validator)
        {
            _validator = validator;
            _authorsRepository = authorsRepository;
        }
        public async Task<Result<Guid, ErrorList>> Handle(
            CreateAuthorCommand command, 
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if(validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var id = AuthorId.NewAuthorId();
            var email = Email.Create(command.Email).Value;
            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
            var fullName = FullName.Create(command.FirstName, command.LastName).Value;

            var author = new Author(id, fullName, email, phoneNumber);

            var authorId = await _authorsRepository.Add(author, cancellationToken);

            return authorId;
        }
    }
}
