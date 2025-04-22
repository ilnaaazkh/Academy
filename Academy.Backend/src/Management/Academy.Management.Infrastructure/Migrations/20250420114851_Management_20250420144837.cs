using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Management.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Management_20250420144837 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "attachments",
                schema: "management",
                table: "authorings",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "attachments",
                schema: "management",
                table: "authorings");
        }
    }
}
