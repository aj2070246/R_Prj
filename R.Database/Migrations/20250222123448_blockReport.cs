using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R.Database.Migrations
{
    /// <inheritdoc />
    public partial class blockReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlockedUserId",
                table: "FavoriteDataLog",
                newName: "FavoritedUserId");

            migrationBuilder.AddColumn<string>(
                name: "RUserId",
                table: "CheckMeActivityLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RUserId",
                table: "CheckMeActivityLogs");

            migrationBuilder.RenameColumn(
                name: "FavoritedUserId",
                table: "FavoriteDataLog",
                newName: "BlockedUserId");
        }
    }
}
