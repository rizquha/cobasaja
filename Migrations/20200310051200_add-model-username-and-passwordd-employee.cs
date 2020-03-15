using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_App.Migrations
{
    public partial class addmodelusernameandpassworddemployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "employees");
        }
    }
}
