using Academy.CourseManagement.Application.DTOs;
using Academy.CourseManagement.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Academy.CourseManagement.Infrastructure.DbContexts
{
    internal class CourseManagementReadDbContext : DbContext, IReadDbContext
    {
        private const string DATABASE = "Database";
        private const string SCHEMA = "course_management";
        private const string PATH_TO_CONFIGURATIONS = "Configurations.Read";
        private readonly IConfiguration _configuration;

        public CourseManagementReadDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IQueryable<CourseDto> Courses => Set<CourseDto>();
        public IQueryable<ModuleDto> Modules => Set<ModuleDto>();
        public IQueryable<LessonDto> Lessons => Set<LessonDto>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_configuration.GetConnectionString(DATABASE))
                .UseLoggerFactory(CreateLoggerFactory())
                .UseSnakeCaseNamingConvention()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseManagementWriteDbContext).Assembly,
                type => type.FullName?.Contains(PATH_TO_CONFIGURATIONS) ?? false);
        }

        private ILoggerFactory CreateLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
        }
    }
}
