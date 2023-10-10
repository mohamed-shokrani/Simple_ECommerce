using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.AppIdentityDb
{
    /// <inheritdoc />
    public partial class AddTimeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdentityUserId",
                table: "ApplicationUserLogins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ApplicationUserLogins",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserLogins_UserId",
                table: "ApplicationUserLogins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserLogins_AspNetUsers_UserId",
                table: "ApplicationUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserLogins_AspNetUsers_UserId",
                table: "ApplicationUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserLogins_UserId",
                table: "ApplicationUserLogins");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "ApplicationUserLogins");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ApplicationUserLogins");
        }
    }
}
