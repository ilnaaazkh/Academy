﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Accounts_RefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "jti",
                schema: "accounts",
                table: "refresh_sessions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "jti",
                schema: "accounts",
                table: "refresh_sessions");
        }
    }
}
