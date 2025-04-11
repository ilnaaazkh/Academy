using Academy.Accounts.Infrastructure.Managers;
using Academy.Accounts.Infrastructure.Models;
using Academy.Accounts.Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Academy.Accounts.Infrastructure.Seeding
{
    public class AccountSeederService
    {
        private readonly AccountsDbContext _accountsDbContext;
        private readonly RoleManager<Role> _roleManager;
        private readonly PermissionManager _permissionManager;
        private readonly RolePermissionManager _rolePermissionManager;
        private readonly UserManager<User> userManager;
        private readonly AdminOptions _adminOptions;

        public AccountSeederService(AccountsDbContext accountsDbContext,
            RoleManager<Role> roleManager,
            PermissionManager permissionManager,
            RolePermissionManager rolePermissionManager,
            UserManager<User> userManager,
            IOptions<AdminOptions> options)
        {
            _accountsDbContext = accountsDbContext;
            _roleManager = roleManager;
            _permissionManager = permissionManager;
            _rolePermissionManager = rolePermissionManager;
            this.userManager = userManager;
            _adminOptions = options.Value;
        }

        public async Task SeedAsync()
        {
            var json = await File.ReadAllTextAsync("etc/accounts.json");

            var seedData = JsonSerializer.Deserialize<RolePermissionConfig>(json)
                ?? throw new ApplicationException("Data for seeding has not been provided");

            var permissionsToAdd = seedData.Permissions.SelectMany(s => s.Value);

            await _permissionManager.AddRange(permissionsToAdd);
            await SeedRoles(seedData);
            await SeedRolePermissions(seedData);

            var isAdminUserExist = await userManager.FindByEmailAsync(_adminOptions.Email) is not null;

            if(isAdminUserExist == false)
            {
                var adminRole = await _roleManager.FindByNameAsync(AdminOptions.ADMIN)
                ?? throw new ApplicationException("Admin role does not exist");

                var admin = User.CreateAdmin(_adminOptions.Email, _adminOptions.Username, adminRole);
                await userManager.CreateAsync(admin, _adminOptions.Password);
            }
        }

        private async Task SeedRolePermissions(RolePermissionConfig seedData)
        {
            foreach (var roleName in seedData.Roles.Keys)
            {
                var role = await _roleManager.FindByNameAsync(roleName)
                    ?? throw new ApplicationException("Try to add rolePermissions to non existing role");

                var rolePermissions = seedData.Roles[roleName];
                await _rolePermissionManager.AddRange(role.Id, rolePermissions);
            }
        }

        private async Task SeedRoles(RolePermissionConfig seedData)
        {
            foreach (var role in seedData.Roles.Keys)
            {
                var existingRole = await _roleManager.FindByNameAsync(role);

                if (existingRole is not null)
                    continue;

                await _roleManager.CreateAsync(new Role { Name = role });
            }
        }
    }
}
