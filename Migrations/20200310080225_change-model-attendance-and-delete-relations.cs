using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_App.Migrations
{
    public partial class changemodelattendanceanddeleterelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendance_employees_EmployeesID",
                table: "attendance");

            migrationBuilder.DropIndex(
                name: "IX_attendance_EmployeesID",
                table: "attendance");

            migrationBuilder.DropColumn(
                name: "EmployeesID",
                table: "attendance");

            migrationBuilder.AddColumn<string>(
                name: "NameEmployee",
                table: "attendance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEmployee",
                table: "attendance");

            migrationBuilder.AddColumn<int>(
                name: "EmployeesID",
                table: "attendance",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_attendance_EmployeesID",
                table: "attendance",
                column: "EmployeesID");

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
