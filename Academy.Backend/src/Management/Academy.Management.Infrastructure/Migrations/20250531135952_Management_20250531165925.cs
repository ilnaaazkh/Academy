using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Management.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Management_20250531165925 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "management",
                table: "authorings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "management",
                table: "authorings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "middle_name",
                schema: "management",
                table: "authorings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "management",
                table: "authorings");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "management",
                table: "authorings");

            migrationBuilder.DropColumn(
                name: "middle_name",
                schema: "management",
                table: "authorings");
        }
    }
}
