using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Accounts_20250530200609 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "accounts",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "accounts",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "middle_name",
                schema: "accounts",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "accounts",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "accounts",
                table: "users");

            migrationBuilder.DropColumn(
                name: "middle_name",
                schema: "accounts",
                table: "users");
        }
    }
}
