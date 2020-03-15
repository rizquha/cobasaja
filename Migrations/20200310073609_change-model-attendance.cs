using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_App.Migrations
{
    public partial class changemodelattendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendance_employees_EmployeesID",
                table: "attendance");

            migrationBuilder.RenameColumn(
                name: "EmployeesID",
                table: "attendance",
                newName: "EmployeesId");

            migrationBuilder.RenameIndex(
                name: "IX_attendance_EmployeesID",
                table: "attendance",
                newName: "IX_attendance_EmployeesId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "attendance",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_attendance_employees_EmployeesId",
                table: "attendance",
                column: "EmployeesId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendance_employees_EmployeesId",
                table: "attendance");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "attendance");

            migrationBuilder.RenameColumn(
                name: "EmployeesId",
                table: "attendance",
                newName: "EmployeesID");

            migrationBuilder.RenameIndex(
                name: "IX_attendance_EmployeesId",
                table: "attendance",
                newName: "IX_attendance_EmployeesID");

            migrationBuilder.AddForeignKey(
                name: "FK_attendance_employees_EmployeesID",
                table: "attendance",
                column: "EmployeesID",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
