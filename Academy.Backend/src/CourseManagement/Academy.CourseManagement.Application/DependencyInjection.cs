using Academy.Core.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace Academy.CourseManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCourseManagementApplication(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan.FromAssemblies(typeof(DependencyInjection).Assembly)
                    .AddClasses(classes =>
                    {
                        classes.AssignableToAny(typeof(ICommandHandler<>), typeof(ICommandHandler<,>));
                    })
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime();
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
