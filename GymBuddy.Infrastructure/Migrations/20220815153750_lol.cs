using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymBuddy.Infrastructure.Migrations
{
    public partial class lol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "Roles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserRoleId",
                table: "Roles",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_UserRoles_UserRoleId",
                table: "Roles",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_UserRoles_UserRoleId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserRoleId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "Roles");
        }
    }
}
