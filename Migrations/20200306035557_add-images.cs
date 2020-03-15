using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_App.Migrations
{
    public partial class addimages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "applicants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "applicants");
        }
    }
}
