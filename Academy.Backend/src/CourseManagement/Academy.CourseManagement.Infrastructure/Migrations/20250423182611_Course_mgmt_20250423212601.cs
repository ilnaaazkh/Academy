using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.CourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Course_mgmt_20250423212601 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                schema: "course_management",
                table: "modules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "course_management",
                table: "modules",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }
    }
}
