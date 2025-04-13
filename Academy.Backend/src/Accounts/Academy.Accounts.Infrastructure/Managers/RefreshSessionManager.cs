using Academy.Accounts.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy.Accounts.Infrastructure.Managers
{
    public class RefreshSessionManager
    {
        private readonly AccountsDbContext _accountsDbContext;

        public RefreshSessionManager(AccountsDbContext accountsDbContext)
        {
            _accountsDbContext = accountsDbContext;
        }

        public async Task CreateRefreshSession(RefreshSession refreshSession, CancellationToken ct)
        {
            await _accountsDbContext.RefreshSessions.AddAsync(refreshSession, ct);
            await _accountsDbContext.SaveChangesAsync(ct);
        }

        public async Task<RefreshSession?> GetByRefreshSession(Guid refreshToken, CancellationToken ct)
        {
            return await _accountsDbContext.RefreshSessions.Include(s => s.User).FirstOrDefaultAsync(r => r.RefreshToken == refreshToken, ct);
        }

        public async Task Delete(RefreshSession refreshSession, CancellationToken ct)
        {
            _accountsDbContext.RefreshSessions.Remove(refreshSession);
            await _accountsDbContext.SaveChangesAsync(ct);
        }
    }
}
