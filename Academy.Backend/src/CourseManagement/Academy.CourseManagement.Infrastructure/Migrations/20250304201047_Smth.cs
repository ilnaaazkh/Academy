using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.CourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Smth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_authorships_author_author_id",
                schema: "course_management",
                table: "authorships");

            migrationBuilder.DropPrimaryKey(
                name: "pk_author",
                schema: "course_management",
                table: "author");

            migrationBuilder.RenameTable(
                name: "author",
                schema: "course_management",
                newName: "authors",
                newSchema: "course_management");

            migrationBuilder.AddPrimaryKey(
                name: "pk_authors",
                schema: "course_management",
                table: "authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_authorships_authors_author_id",
                schema: "course_management",
                table: "authorships",
                column: "author_id",
                principalSchema: "course_management",
                principalTable: "authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_authorships_authors_author_id",
                schema: "course_management",
                table: "authorships");

            migrationBuilder.DropPrimaryKey(
                name: "pk_authors",
                schema: "course_management",
                table: "authors");

            migrationBuilder.RenameTable(
                name: "authors",
                schema: "course_management",
                newName: "author",
                newSchema: "course_management");

            migrationBuilder.AddPrimaryKey(
                name: "pk_author",
                schema: "course_management",
                table: "author",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_authorships_author_author_id",
                schema: "course_management",
                table: "authorships",
                column: "author_id",
                principalSchema: "course_management",
                principalTable: "author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
