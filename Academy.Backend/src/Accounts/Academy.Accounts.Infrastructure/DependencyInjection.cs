using Academy.Accounts.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Accounts.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AccountsDbContext>();
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AccountsDbContext>();

            return services;
        }
    }
}
