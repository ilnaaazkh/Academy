using Academy.CourseManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Academy.CourseManagement.Infrastructure.DbContexts
{
    public class CourseManagementWriteDbContext : DbContext
    {
        private const string DATABASE = "Database";
        private const string SCHEMA = "course_management";
        private const string PATH_TO_CONFIGURATIONS = "Configurations.Write";
        private readonly IConfiguration _configuration;

        public DbSet<Course> Courses { get; set; }
        
        public CourseManagementWriteDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_configuration.GetConnectionString(DATABASE))
                .UseLoggerFactory(CreateLoggerFactory())
                .UseSnakeCaseNamingConvention();
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
