using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.CourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Course_mgmt_20250508153915 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "preview",
                schema: "course_management",
                table: "courses",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "preview",
                schema: "course_management",
                table: "courses");
        }
    }
}
