using Academy.Accounts.Contracts;
using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.Management.Domain;
using CSharpFunctionalExtensions;

namespace Academy.Management.Application.Authorings.Command.CreateAuthoring
{
    public class CreateAuthoringCommandHandler : ICommandHandler<Guid, CreteAuthoringCommand>
    {
        private readonly IAuthoringsRepository _authorRoleRequestRepository;

        public CreateAuthoringCommandHandler(
            IAuthoringsRepository authorRoleRequestRepository,
            IAccountsContract accountsContract)
        {
            _authorRoleRequestRepository = authorRoleRequestRepository;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            CreteAuthoringCommand command,
            CancellationToken cancellationToken = default)
        {
            var id = Guid.NewGuid();
            var authorRoleRequest = new Authoring(id, command.UserId, command.Comment);

            await _authorRoleRequestRepository.Add(authorRoleRequest, cancellationToken);
            await _authorRoleRequestRepository.Save(authorRoleRequest, cancellationToken);

            return id;
        }
    }

}
