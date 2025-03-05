using Academy.CourseManagement.Application.Authors;
using Academy.CourseManagement.Infrastructure.DbContexts;
using Academy.CourseManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.CourseManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCourseManagementInfrastructure(
            this IServiceCollection services)
        {
            services.AddDbContext<CourseManagementWriteDbContext>();
            services.AddScoped<IAuthorsRepository, AuthorsRepository>();
            return services;
        }
    }
}
