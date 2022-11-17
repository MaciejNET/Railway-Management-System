using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayManagementSystem.Api.Migrations
{
    public partial class RemovePdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfVersion",
                table: "Tickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PdfVersion",
                table: "Tickets",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
