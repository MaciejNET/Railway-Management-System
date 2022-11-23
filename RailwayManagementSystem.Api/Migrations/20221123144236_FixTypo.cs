using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RailwayManagementSystem.Api.Migrations
{
    public partial class FixTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trains_Careers_CareerId",
                table: "Trains");

            migrationBuilder.DropTable(
                name: "Careers");

            migrationBuilder.RenameColumn(
                name: "CareerId",
                table: "Trains",
                newName: "CarrierId");

            migrationBuilder.RenameIndex(
                name: "IX_Trains_CareerId",
                table: "Trains",
                newName: "IX_Trains_CarrierId");

            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Trains_Carriers_CarrierId",
                table: "Trains",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trains_Carriers_CarrierId",
                table: "Trains");

            migrationBuilder.DropTable(
                name: "Carriers");

            migrationBuilder.RenameColumn(
                name: "CarrierId",
                table: "Trains",
                newName: "CareerId");

            migrationBuilder.RenameIndex(
                name: "IX_Trains_CarrierId",
                table: "Trains",
                newName: "IX_Trains_CareerId");

            migrationBuilder.CreateTable(
                name: "Careers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Careers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Trains_Careers_CareerId",
                table: "Trains",
                column: "CareerId",
                principalTable: "Careers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
