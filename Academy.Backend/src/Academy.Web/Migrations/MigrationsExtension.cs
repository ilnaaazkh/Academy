using Academy.Accounts.Infrastructure;
using Academy.CourseManagement.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Academy.Web.Migrations
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<CourseManagementWriteDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
