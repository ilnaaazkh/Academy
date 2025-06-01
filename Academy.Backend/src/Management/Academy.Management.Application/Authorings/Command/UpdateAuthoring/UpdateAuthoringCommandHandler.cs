using Academy.Accounts.Contracts;
using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.Management.Application.Authorings.Command.UpdateAuthoring
{
    public class UpdateAuthoringCommandHandler : ICommandHandler<Guid, UpdateAuthoringCommand>
    {
        private readonly IAuthoringsRepository _authorRoleRequestRepository;

        public UpdateAuthoringCommandHandler(
            IAuthoringsRepository authorRoleRequestRepository,
            IAccountsContract accountsContract)
        {
            _authorRoleRequestRepository = authorRoleRequestRepository;
        }

        public async Task<Result<Guid, ErrorList>> Handle(UpdateAuthoringCommand command, CancellationToken cancellationToken = default)
        {
            var authoring = await _authorRoleRequestRepository.GetById(command.AuthoringId, cancellationToken);

            if (authoring == null)
                return Errors.General.NotFound(command.AuthoringId).ToErrorList();

            if (authoring.UserId != command.UserId)
                return Errors.User.AccessDenied().ToErrorList();

            authoring.SetComment(command.Comment);
            authoring.SetName(command.FirstName, command.LastName, command.MiddleName);

            await _authorRoleRequestRepository.Save(authoring, cancellationToken);

            return authoring.Id;
        }
    }
}
