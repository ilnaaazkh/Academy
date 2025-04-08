using Academy.Accounts.Infrastructure.Managers;
using Academy.Accounts.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Academy.Accounts.Infrastructure.Seeding
{
    public class RolePermissionConfig 
    {
        public Dictionary<string, string[]> Permissions { get; set; } = [];
        public Dictionary<string, string[]> Roles { get; set; } = [];
    }
    public class AccountsSeeder
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AccountsSeeder(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task SeedAsync()
        {
            var json = await File.ReadAllTextAsync("etc/accounts.json");

            var scope = _serviceScopeFactory.CreateScope();
            var accountsDbContext = scope.ServiceProvider.GetRequiredService<AccountsDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var permissionManager = scope.ServiceProvider.GetRequiredService<PermissionManager>();
            var rolePermissionManager = scope.ServiceProvider.GetRequiredService<RolePermissionManager>();

            var seedData = JsonSerializer.Deserialize<RolePermissionConfig>(json)
                ?? throw new ApplicationException("Data for seeding has not been provided");

            var permissionsToAdd = seedData.Permissions.SelectMany(s => s.Value);

            await permissionManager.AddRange(permissionsToAdd);
            await SeedRoles(roleManager, seedData);
            await SeedRolePermissions(roleManager, rolePermissionManager, seedData);
        }

        private async Task SeedRolePermissions(RoleManager<Role> roleManager, RolePermissionManager rolePermissionManager, RolePermissionConfig seedData)
        {
            foreach (var roleName in seedData.Roles.Keys)
            {
                var role = await roleManager.FindByNameAsync(roleName)
                    ?? throw new ApplicationException("Try to add rolePermissions to non existing role");

                var rolePermissions = seedData.Roles[roleName];
                await rolePermissionManager.AddRange(role.Id, rolePermissions);
            }
        }

        private async Task SeedRoles(RoleManager<Role> roleManager, RolePermissionConfig seedData)
        {
            foreach (var role in seedData.Roles.Keys)
            {
                var existingRole = await roleManager.FindByNameAsync(role);

                if (existingRole is not null)
                    continue;

                await roleManager.CreateAsync(new Role { Name = role });
            }
        }
    }
}
