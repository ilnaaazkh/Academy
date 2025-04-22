using Academy.Management.Domain;
using Academy.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

            builder.Entity<Authoring>()
                .Property(a => a.Attachments)
                .HasConversion(
                    a => JsonConvert.SerializeObject(a),
                    v => JsonConvert.DeserializeObject<IReadOnlyList<Attachment>>(v)!,
                    new ValueComparer<IReadOnlyList<Attachment>>(
                        (c1, c2) => c1!.SequenceEqual(c2!),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList())
                    )
                .HasColumnType("jsonb");
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => builder.AddConsole());
    }
}
