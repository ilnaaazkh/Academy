using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.CourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NotReqPracticeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "practice_lesson_data",
                schema: "course_management",
                table: "lessons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "practice_lesson_data",
                schema: "course_management",
                table: "lessons",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
