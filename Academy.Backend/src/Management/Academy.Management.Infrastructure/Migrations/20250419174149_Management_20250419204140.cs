using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Management.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Management_20250419204140 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                schema: "management",
                table: "authorings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "management",
                table: "authorings");
        }
    }
}
