using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.Accounts.Contracts
{
    public interface IAccountsContract
    {
        Task<UnitResult<ErrorList>> ApproveAuthoringRequest(Guid userId, CancellationToken cancellationToken);
    }
}
