using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R.Database.Migrations
{
    /// <inheritdoc />
    public partial class blockReport3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckMeActivityLogs_Users_RUsersId",
                table: "CheckMeActivityLogs");

            migrationBuilder.AlterColumn<string>(
                name: "RUsersId",
                table: "CheckMeActivityLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckMeActivityLogs_Users_RUsersId",
                table: "CheckMeActivityLogs",
                column: "RUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckMeActivityLogs_Users_RUsersId",
                table: "CheckMeActivityLogs");

            migrationBuilder.AlterColumn<string>(
                name: "RUsersId",
                table: "CheckMeActivityLogs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckMeActivityLogs_Users_RUsersId",
                table: "CheckMeActivityLogs",
                column: "RUsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
