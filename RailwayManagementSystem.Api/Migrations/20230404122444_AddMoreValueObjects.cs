using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RailwayManagementSystem.Api.Migrations
{
    public partial class AddMoreValueObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_TripIntervals_TripIntervalId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "TripIntervals");

            migrationBuilder.DropIndex(
                name: "IX_Trips_TripIntervalId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripIntervalId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "Name_Value",
                table: "Trains",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Name_Value",
                table: "Stations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "City_Value",
                table: "Stations",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Name_Value",
                table: "RailwayEmployees",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LastName_Value",
                table: "RailwayEmployees",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FirstName_Value",
                table: "RailwayEmployees",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber_Value",
                table: "Passengers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "LastName_Value",
                table: "Passengers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FirstName_Value",
                table: "Passengers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Email_Value",
                table: "Passengers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Name_Value",
                table: "Discounts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Name_Value",
                table: "Carriers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Name_Value",
                table: "Admins",
                newName: "Name");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Trains",
                newName: "Name_Value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Stations",
                newName: "Name_Value");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Stations",
                newName: "City_Value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RailwayEmployees",
                newName: "Name_Value");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "RailwayEmployees",
                newName: "LastName_Value");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "RailwayEmployees",
                newName: "FirstName_Value");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Passengers",
                newName: "PhoneNumber_Value");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Passengers",
                newName: "LastName_Value");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Passengers",
                newName: "FirstName_Value");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Passengers",
                newName: "Email_Value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Discounts",
                newName: "Name_Value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Carriers",
                newName: "Name_Value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Admins",
                newName: "Name_Value");

            migrationBuilder.AddColumn<int>(
                name: "TripIntervalId",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TripIntervals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Friday = table.Column<bool>(type: "boolean", nullable: false),
                    Monday = table.Column<bool>(type: "boolean", nullable: false),
                    Saturday = table.Column<bool>(type: "boolean", nullable: false),
                    Sunday = table.Column<bool>(type: "boolean", nullable: false),
                    Thursday = table.Column<bool>(type: "boolean", nullable: false),
                    Tuesday = table.Column<bool>(type: "boolean", nullable: false),
                    Wednesday = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripIntervals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TripIntervalId",
                table: "Trips",
                column: "TripIntervalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_TripIntervals_TripIntervalId",
                table: "Trips",
                column: "TripIntervalId",
                principalTable: "TripIntervals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
