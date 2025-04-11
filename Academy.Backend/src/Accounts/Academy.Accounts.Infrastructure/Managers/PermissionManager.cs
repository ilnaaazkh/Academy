using Academy.Accounts.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;
namespace Academy.Accounts.Infrastructure.Managers
{
    public class PermissionManager
    {
        private readonly AccountsDbContext _dbContext;

        public PermissionManager(AccountsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds a collection of permission codes to the database if they do not already exist.
        /// </summary>
        /// <param name="permissionsToAdd">A collection of permission codes to add.</param>
        /// <remarks>
        /// This method checks each permission code in the provided collection. 
        /// If a permission with the same code already exists in the database, it will be skipped.
        /// Only unique, non-existing permissions will be added.
        /// </remarks>
        /// <exception cref="DbUpdateException">Thrown if there is an error saving changes to the database.</exception>
        public async Task AddRange(IEnumerable<string> permissionsToAdd)
        {
            foreach (var permissionCode in permissionsToAdd)
            {
                bool isPermissionExist = await _dbContext.Permissions.AnyAsync(p => p.Code == permissionCode);

                if (isPermissionExist)
                {
                    continue;
                }

                var permission = new Permission(permissionCode);
                await _dbContext.Permissions.AddAsync(permission);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Permission?> GetPermissionByCode(string code)
        {
            return await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<IReadOnlyList<Permission>> GetPermissions(Guid userId)
        {
            return await _dbContext.Users
                                    .Where(u => u.Id == userId)
                                    .SelectMany(u => u.Roles.SelectMany(r => r.Permissions))
                                    .Distinct()
                                    .AsNoTracking()
                                    .ToListAsync();
        }
    }
}
