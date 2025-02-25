using Academy.CourseManagement.Infrastructure.DbContexts;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.CourseManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCourseManagementInfrastructure(
            this IServiceCollection services)
        {
            services.AddDbContext<CourseManagementWriteDbContext>();
            
            return services;
        }
    }
}
