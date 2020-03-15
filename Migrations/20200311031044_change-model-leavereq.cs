using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_App.Migrations
{
    public partial class changemodelleavereq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leaveRequests_leaveRequests_LeaveReqId",
                table: "leaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_leaveRequests_LeaveReqId",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "LeaveReqId",
                table: "leaveRequests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveReqId",
                table: "leaveRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_leaveRequests_LeaveReqId",
                table: "leaveRequests",
                column: "LeaveReqId");

            migrationBuilder.AddForeignKey(
                name: "FK_leaveRequests_leaveRequests_LeaveReqId",
                table: "leaveRequests",
                column: "LeaveReqId",
                principalTable: "leaveRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
