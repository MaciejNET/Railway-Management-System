using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayManagementSystem.Infrastructure.Migrations
{
    public partial class AddRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Passengers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Admins",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Admins");
        }
    }
}
