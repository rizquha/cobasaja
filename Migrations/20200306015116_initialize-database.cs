using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_App.Migrations
{
    public partial class initializedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CV = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    BirthPlace = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    Departement = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    NameEmergencyContact1 = table.Column<string>(nullable: true),
                    PhoneEmergencyContact1 = table.Column<string>(nullable: true),
                    NameEmergencyContact2 = table.Column<string>(nullable: true),
                    PhoneEmergencyContact2 = table.Column<string>(nullable: true),
                    NameEmergencyContact3 = table.Column<string>(nullable: true),
                    PhoneEmergencyContact3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    BirthPlace = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    Departement = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    NameEmergencyContact1 = table.Column<string>(nullable: true),
                    PhoneEmergencyContact1 = table.Column<string>(nullable: true),
                    NameEmergencyContact2 = table.Column<string>(nullable: true),
                    PhoneEmergencyContact2 = table.Column<string>(nullable: true),
                    NameEmergencyContact3 = table.Column<string>(nullable: true),
                    PhoneEmergencyContact3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "attendance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Presence = table.Column<DateTime>(nullable: false),
                    Absent = table.Column<bool>(nullable: false),
                    Present = table.Column<bool>(nullable: false),
                    EmployeesID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attendance_employees_EmployeesID",
                        column: x => x.EmployeesID,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "leaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    LeaveReqId = table.Column<int>(nullable: true),
                    EmployeesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leaveRequests_employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_leaveRequests_leaveRequests_LeaveReqId",
                        column: x => x.LeaveReqId,
                        principalTable: "leaveRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attendance_EmployeesID",
                table: "attendance",
                column: "EmployeesID");

            migrationBuilder.CreateIndex(
                name: "IX_leaveRequests_EmployeesId",
                table: "leaveRequests",
                column: "EmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_leaveRequests_LeaveReqId",
                table: "leaveRequests",
                column: "LeaveReqId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicants");

            migrationBuilder.DropTable(
                name: "attendance");

            migrationBuilder.DropTable(
                name: "leaveRequests");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}
