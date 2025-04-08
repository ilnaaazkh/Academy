using Academy.Accounts.Infrastructure.Managers;
using Academy.Accounts.Infrastructure.Models;
using Academy.Accounts.Infrastructure.Options;
using Academy.Accounts.Infrastructure.Providers;
using Academy.Accounts.Infrastructure.Seeding;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Accounts.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddOptions<JwtOptions>()
               .BindConfiguration(JwtOptions.JWT);

            services.AddDbContext<AccountsDbContext>();
            services.AddIdentity<User, Role>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AccountsDbContext>();

            services.AddTransient<JwtProvider>();

            services.AddSingleton<AccountsSeeder>();
            services.AddScoped<RolePermissionManager>();
            services.AddScoped<PermissionManager>();

            return services;
        }
    }
}
