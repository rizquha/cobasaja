using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_App.Migrations
{
    public partial class changemodelleaverequestnorelationsagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emailEmployee",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "nameEmployee",
                table: "leaveRequests");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "leaveRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "leaveRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "leaveRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "leaveRequests");

            migrationBuilder.AddColumn<string>(
                name: "emailEmployee",
                table: "leaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nameEmployee",
                table: "leaveRequests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
