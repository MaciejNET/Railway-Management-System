using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayManagementSystem.Infrastructure.Migrations
{
    public partial class ChangeInPassengerAndTripSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Stations_StationId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_StationId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_TripId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TripInterval_Friday",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripInterval_Monday",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripInterval_Saturday",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripInterval_Sunday",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripInterval_Thursday",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripInterval_Tuesday",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripInterval_Wednesday",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Passengers");

            migrationBuilder.AddColumn<bool>(
                name: "TripAvailability_Friday",
                table: "Schedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TripAvailability_Monday",
                table: "Schedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TripAvailability_Saturday",
                table: "Schedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TripAvailability_Sunday",
                table: "Schedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TripAvailability_Thursday",
                table: "Schedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TripAvailability_Tuesday",
                table: "Schedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TripAvailability_Wednesday",
                table: "Schedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ValidDate_From",
                table: "Schedules",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "ValidDate_To",
                table: "Schedules",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Passengers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "StationSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArrivalTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    DepartureTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Platform = table.Column<int>(type: "integer", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StationSchedules_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StationSchedules_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TripId",
                table: "Schedules",
                column: "TripId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StationSchedules_ScheduleId",
                table: "StationSchedules",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_StationSchedules_StationId",
                table: "StationSchedules",
                column: "StationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StationSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_TripId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TripAvailability_Friday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TripAvailability_Monday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TripAvailability_Saturday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TripAvailability_Sunday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TripAvailability_Thursday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TripAvailability_Tuesday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TripAvailability_Wednesday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ValidDate_From",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ValidDate_To",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Passengers");

            migrationBuilder.AddColumn<bool>(
                name: "TripInterval_Friday",
                table: "Trips",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TripInterval_Monday",
                table: "Trips",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TripInterval_Saturday",
                table: "Trips",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TripInterval_Sunday",
                table: "Trips",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TripInterval_Thursday",
                table: "Trips",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TripInterval_Tuesday",
                table: "Trips",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TripInterval_Wednesday",
                table: "Trips",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "Schedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "Schedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Platform",
                table: "Schedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "StationId",
                table: "Schedules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Passengers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Passengers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_StationId",
                table: "Schedules",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TripId",
                table: "Schedules",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Stations_StationId",
                table: "Schedules",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
