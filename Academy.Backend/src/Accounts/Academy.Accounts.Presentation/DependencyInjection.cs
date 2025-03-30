using Academy.Accounts.Application;
using Academy.Accounts.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Academy.Accounts.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAccountsService(this IServiceCollection services)
        {
            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.JWT)
                .ValidateDataAnnotations()
                .ValidateOnStart();
                

            services.AddAuthentication();

            services.AddAuthorization();

            services
                .AddApplication()
                .AddInfrastructure();
            

            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                var jwtOptions = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value;

                                options.TokenValidationParameters = new TokenValidationParameters()
                                {
                                    ValidIssuer = "test",
                                    ValidAudience = "test",
                                    IssuerSigningKey = 
                                        new SymmetricSecurityKey("jbcjdbcjsbhjsdchjsdbcsdcsdcds"u8.ToArray()),
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    ValidateLifetime = false,
                                    ValidateIssuerSigningKey = false,
                                    ClockSkew = TimeSpan.Zero,
                                };

                            });

            return services;
        }
    }
}
