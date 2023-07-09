using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayManagementSystem.Infrastructure.Migrations
{
    public partial class ChangeInStationAndTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Tickets_TicketId",
                table: "Stations");

            migrationBuilder.DropIndex(
                name: "IX_Stations_TicketId",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Stations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TripDate",
                table: "Tickets",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "TicketStation",
                columns: table => new
                {
                    StationsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStation", x => new { x.StationsId, x.TicketsId });
                    table.ForeignKey(
                        name: "FK_TicketStation_Stations_StationsId",
                        column: x => x.StationsId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketStation_Tickets_TicketsId",
                        column: x => x.TicketsId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketStation_TicketsId",
                table: "TicketStation",
                column: "TicketsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketStation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TripDate",
                table: "Tickets",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketId",
                table: "Stations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stations_TicketId",
                table: "Stations",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Tickets_TicketId",
                table: "Stations",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }
    }
}
