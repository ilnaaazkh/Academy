using Academy.CourseManagement.Application.Authors;
using Academy.CourseManagement.Application.Authorships;
using Academy.CourseManagement.Application.Courses;
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

            services.AddScoped<IAuthorsRepository, AuthorsRepository>();
            services.AddScoped<ICoursesRepository, CoursesRepository>();
            services.AddScoped<IAuthorshipsRepository, AuthorshipsRepository>();

            return services;
        }
    }
}
