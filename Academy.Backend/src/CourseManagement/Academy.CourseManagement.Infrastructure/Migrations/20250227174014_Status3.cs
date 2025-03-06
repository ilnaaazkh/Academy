using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.CourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Status3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_lessons_modules_module_id",
                schema: "course_management",
                table: "lessons");

            migrationBuilder.DropForeignKey(
                name: "fk_modules_courses_course_id",
                schema: "course_management",
                table: "modules");

            migrationBuilder.AddForeignKey(
                name: "fk_lessons_modules_module_id",
                schema: "course_management",
                table: "lessons",
                column: "module_id",
                principalSchema: "course_management",
                principalTable: "modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_modules_courses_course_id",
                schema: "course_management",
                table: "modules",
                column: "course_id",
                principalSchema: "course_management",
                principalTable: "courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_lessons_modules_module_id",
                schema: "course_management",
                table: "lessons");

            migrationBuilder.DropForeignKey(
                name: "fk_modules_courses_course_id",
                schema: "course_management",
                table: "modules");

            migrationBuilder.AddForeignKey(
                name: "fk_lessons_modules_module_id",
                schema: "course_management",
                table: "lessons",
                column: "module_id",
                principalSchema: "course_management",
                principalTable: "modules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_modules_courses_course_id",
                schema: "course_management",
                table: "modules",
                column: "course_id",
                principalSchema: "course_management",
                principalTable: "courses",
                principalColumn: "Id");
        }
    }
}
