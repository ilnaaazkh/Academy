using Academy.Management.Application.Authorings;
using Academy.Management.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Management.Infrastructure
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ManagementDbContext>();
            services.AddScoped<IAuthoringsRepository, AuthoringsRepository>();

            return services;
        }
    }
}
