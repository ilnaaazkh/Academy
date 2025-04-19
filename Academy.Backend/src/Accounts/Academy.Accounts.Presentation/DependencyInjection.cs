using Academy.Accounts.Application;
using Academy.Accounts.Infrastructure;
using Academy.Accounts.Infrastructure.Factories;
using Academy.Accounts.Infrastructure.Options;
using Academy.Framework.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Accounts.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAccountsService(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddApplication()
                .AddInfrastructure();

            services.AddAuthentication(configuration);
            services.AddAuthorization();

            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(JwtOptions.JWT).Get<JwtOptions>()
                   ?? throw new ApplicationException("Missing jwt configuration");

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme; 
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = TokenValidationParametersFactory.Create(jwtOptions);
                    options.MapInboundClaims = false;
                });

            return services;
        }
    }
}
