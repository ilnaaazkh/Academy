using Academy.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Management.Application
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

            return services;
        }
    }
}
