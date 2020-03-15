using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_App.Migrations
{
    public partial class addmodelattendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Presence",
                table: "attendance");

            migrationBuilder.AddColumn<DateTime>(
                name: "PresenceIn",
                table: "attendance",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PresenceOut",
                table: "attendance",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PresenceIn",
                table: "attendance");

            migrationBuilder.DropColumn(
                name: "PresenceOut",
                table: "attendance");

            migrationBuilder.AddColumn<DateTime>(
                name: "Presence",
                table: "attendance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
