using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.CourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "course_management");

            migrationBuilder.CreateTable(
                name: "authors",
                schema: "course_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    bio = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    social_links = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                schema: "course_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_courses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "authorships",
                schema: "course_management",
                columns: table => new
                {
                    course_id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authorships", x => new { x.course_id, x.author_id });
                    table.ForeignKey(
                        name: "fk_authorships_authors_author_id",
                        column: x => x.author_id,
                        principalSchema: "course_management",
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_authorships_courses_course_id",
                        column: x => x.course_id,
                        principalSchema: "course_management",
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "modules",
                schema: "course_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false),
                    course_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_modules", x => x.id);
                    table.ForeignKey(
                        name: "fk_modules_courses_course_id",
                        column: x => x.course_id,
                        principalSchema: "course_management",
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lessons",
                schema: "course_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    lesson_type = table.Column<string>(type: "text", nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false),
                    questions = table.Column<string>(type: "jsonb", nullable: false),
                    attachments = table.Column<string>(type: "text", nullable: false),
                    practice_lesson_data = table.Column<string>(type: "text", nullable: true),
                    module_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lessons", x => x.id);
                    table.ForeignKey(
                        name: "fk_lessons_modules_module_id",
                        column: x => x.module_id,
                        principalSchema: "course_management",
                        principalTable: "modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_authorships_author_id",
                schema: "course_management",
                table: "authorships",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_lessons_module_id",
                schema: "course_management",
                table: "lessons",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "ix_modules_course_id",
                schema: "course_management",
                table: "modules",
                column: "course_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authorships",
                schema: "course_management");

            migrationBuilder.DropTable(
                name: "lessons",
                schema: "course_management");

            migrationBuilder.DropTable(
                name: "authors",
                schema: "course_management");

            migrationBuilder.DropTable(
                name: "modules",
                schema: "course_management");

            migrationBuilder.DropTable(
                name: "courses",
                schema: "course_management");
        }
    }
}
