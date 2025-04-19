using Academy.Accounts.Infrastructure.Migrations;
using Academy.Accounts.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Academy.Accounts.Infrastructure.Managers
{
    public class RolePermissionManager
    {
        private readonly AccountsDbContext _dbContext;

        public RolePermissionManager(AccountsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRange(Guid roleId, IEnumerable<string> permissions, CancellationToken ct)
        {
            foreach (var permissionCode in permissions)
            {
                var permission = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Code == permissionCode, ct)
                    ?? throw new ApplicationException("Try to add non existing permissions to role");

                var isRolePermissionExist = await _dbContext.RolePermissions
                    .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission.Id, ct);

                if (isRolePermissionExist)
                    continue;

                await _dbContext.RolePermissions.AddAsync(new RolePermission(roleId, permission.Id), ct);
            }

            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
