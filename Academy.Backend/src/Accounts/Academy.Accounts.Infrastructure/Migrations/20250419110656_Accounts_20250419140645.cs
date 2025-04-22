using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Accounts_20250419140645 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "number",
                schema: "accounts",
                table: "roles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "number",
                schema: "accounts",
                table: "roles");
        }
    }
}
