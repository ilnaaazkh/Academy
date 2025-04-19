namespace Academy.Accounts.Contracts
{
    public interface IAccountsContract
    {
        Task<bool> IsUserExist(Guid userId, CancellationToken cancellationToken);
    }
}
