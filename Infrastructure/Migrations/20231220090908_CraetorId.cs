using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class CraetorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_UserId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Events",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_UserId",
                table: "Events",
                newName: "IX_Events_CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_CreatorId",
                table: "Events",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_CreatorId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Events",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_CreatorId",
                table: "Events",
                newName: "IX_Events_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_UserId",
                table: "Events",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
