using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Academy.CourseManagement.Application.Authors.Update.UpdateMainInfo
{
    public class UpdateMainInfoCommandHandler : ICommandHandler<Guid, UpdateMainInfoCommand>
    {
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IValidator<UpdateMainInfoCommand> _validator;

        public UpdateMainInfoCommandHandler(
            IAuthorsRepository authorsRepository,
            IValidator<UpdateMainInfoCommand> validator)
        {
            _authorsRepository = authorsRepository;
            _validator = validator;
        }
        public async Task<Result<Guid, ErrorList>> Handle(UpdateMainInfoCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var authorId = AuthorId.Create(command.Id);
            var authorResult = await _authorsRepository.GetById(authorId, cancellationToken);

            if (authorResult.IsFailure)
            {
                return authorResult.Error.ToErrorList();
            }

            var author = authorResult.Value;
            var email = Email.Create(command.Email).Value;
            var phone = PhoneNumber.Create(command.PhoneNumber).Value;
            var fullName = FullName.Create(command.FirstName, command.LastName).Value;

            var updateResult = author.UpdateMainInfo(fullName, email, phone);

            var saveResult = await _authorsRepository.Save(author, cancellationToken);

            return saveResult.Value;
        }
    }
}
