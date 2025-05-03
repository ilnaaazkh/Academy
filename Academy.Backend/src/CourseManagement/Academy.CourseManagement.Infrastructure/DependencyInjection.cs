using Academy.CourseManagement.Application.Courses;
using Academy.CourseManagement.Application.Courses.CodeExecution;
using Academy.CourseManagement.Application.Interfaces;
using Academy.CourseManagement.Infrastructure.CodeRunner;
using Academy.CourseManagement.Infrastructure.DbContexts;
using Academy.CourseManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.CourseManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddDbContext<CourseManagementWriteDbContext>();

            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            services.AddDbContext<CourseManagementWriteDbContext>();
            services.AddDbContext<IReadDbContext, CourseManagementReadDbContext>();

            services.AddScoped<ICoursesRepository, CoursesRepository>();
            services.AddScoped<ICodeRunner, DockerCodeRunner>();

            return services;
        }
    }
}
