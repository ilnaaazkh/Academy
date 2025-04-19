using Academy.Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Academy.Management.Infrastructure
{
    internal class ManagementDbContext : DbContext
    {
        private const string SCHEMA = "management";
        private const string DATABASE = "Database";

        private readonly IConfiguration _configuration;

        public DbSet<Authoring> Authorings { get; set; }

        public ManagementDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString(DATABASE));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(SCHEMA);

            builder.Entity<Authoring>().HasKey(a => a.Id);

            builder.Entity<Authoring>().Property(a => a.UserId);
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => builder.AddConsole());
    }
}
