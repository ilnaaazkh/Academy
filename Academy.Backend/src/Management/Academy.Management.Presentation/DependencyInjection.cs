using Academy.Management.Application;
using Academy.Management.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Management.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddManagementService(this IServiceCollection services)
        {
            services.AddApplication()
                .AddInfrastructure();

            return services;
        }
    }
}
