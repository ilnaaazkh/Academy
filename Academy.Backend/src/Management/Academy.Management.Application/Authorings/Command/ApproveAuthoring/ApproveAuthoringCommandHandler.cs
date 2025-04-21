using Academy.Accounts.Contracts;
using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.Management.Application.Authorings.Command.ApproveAuthoring
{
    public class ApproveAuthoringCommandHandler : ICommandHandler<ApproveAuthoringCommand>
    {
        private readonly IAuthoringsRepository _authoringsRepository;
        private readonly IAccountsContract _accountsContract;

        public ApproveAuthoringCommandHandler(
            IAuthoringsRepository authoringsRepository,
            IAccountsContract accountsContract)
        {
            _authoringsRepository = authoringsRepository;
            _accountsContract = accountsContract;
        }

        public async Task<UnitResult<ErrorList>> Handle(ApproveAuthoringCommand command, CancellationToken cancellationToken = default)
        {
            var authoring = await _authoringsRepository.GetById(command.AuthoringId, cancellationToken);

            if (authoring is null)
            {
                return Errors.General.NotFound(command.AuthoringId).ToErrorList();
            }

            var addRoleResult = await _accountsContract.ApproveAuthoringRequest(authoring.UserId, cancellationToken);

            if (addRoleResult.IsFailure)
                return addRoleResult.Error;

            var result = authoring.Approve();

            if (result.IsFailure)
                return result.Error.ToErrorList();

            await _authoringsRepository.Save(authoring, cancellationToken);

            return UnitResult.Success<ErrorList>();
        }
    }
}
