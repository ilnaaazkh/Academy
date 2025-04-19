using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Management.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Management_20250419182354 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "management");

            migrationBuilder.RenameTable(
                name: "authorings",
                schema: "accounts",
                newName: "authorings",
                newSchema: "management");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "accounts");

            migrationBuilder.RenameTable(
                name: "authorings",
                schema: "management",
                newName: "authorings",
                newSchema: "accounts");
        }
    }
}
