using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_App.Migrations
{
    public partial class deleteattributeEmployeeID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "attendance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "attendance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
