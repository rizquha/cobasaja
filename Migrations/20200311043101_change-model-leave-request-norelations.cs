using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_App.Migrations
{
    public partial class changemodelleaverequestnorelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leaveRequests_employees_EmployeesId",
                table: "leaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_leaveRequests_EmployeesId",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "EmployeesId",
                table: "leaveRequests");

            migrationBuilder.AddColumn<string>(
                name: "Departement",
                table: "leaveRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "leaveRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "leaveRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "leaveRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "emailEmployee",
                table: "leaveRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nameEmployee",
                table: "leaveRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "leaveRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Departement",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "emailEmployee",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "nameEmployee",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "reason",
                table: "leaveRequests");

            migrationBuilder.AddColumn<int>(
                name: "EmployeesId",
                table: "leaveRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_leaveRequests_EmployeesId",
                table: "leaveRequests",
                column: "EmployeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_leaveRequests_employees_EmployeesId",
                table: "leaveRequests",
                column: "EmployeesId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
