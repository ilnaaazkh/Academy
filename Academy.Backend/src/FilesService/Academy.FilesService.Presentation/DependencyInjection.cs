using Academy.FilesService.Infractructure;
using Academy.FilesService.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace Academy.FilesService.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFilesService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileProvider, MinioProvider>();
            services.AddScoped<Services.FilesService>();
            return services.AddMinio(configuration);
        }

        public static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.MINIO));

            services.AddMinio(options =>
            {
                var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                   ?? throw new ApplicationException("Missing minio configuration");

                options.WithEndpoint(minioOptions.Endpoint);
                options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
                options.WithSSL(minioOptions.WithSSL);
            });
            
            return services;
        }
    }
}
