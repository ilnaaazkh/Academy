using Microsoft.Extensions.DependencyInjection;
using Academy.CourseManagement.Application;
using Academy.CourseManagement.Infrastructure;

namespace Academy.CourseManagement.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCourseManagementModule(this IServiceCollection services)
        {
            return services.AddApplication()
                        .AddInfrastructure();
        }
    }
}
