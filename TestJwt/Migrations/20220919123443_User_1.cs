using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestJwt.Migrations
{
    public partial class User_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created_RefreshToken",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Refresh_Token",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Refresh_Token",
                table: "Users");
        }
    }
}
