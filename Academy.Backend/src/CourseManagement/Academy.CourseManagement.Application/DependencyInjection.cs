using Academy.Core.Abstractions;
using Academy.CourseManagement.Application.Courses.CodeExecution;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace Academy.CourseManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
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

            services.Scan(scan =>
            {
                scan.FromAssemblies(typeof(DependencyInjection).Assembly)
                    .AddClasses(classes =>
                    {
                        classes.AssignableTo(typeof(IQueryHandler<,>));
                    })
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime();
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
