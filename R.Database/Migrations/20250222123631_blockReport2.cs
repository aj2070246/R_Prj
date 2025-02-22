using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R.Database.Migrations
{
    /// <inheritdoc />
    public partial class blockReport2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RUserId",
                table: "CheckMeActivityLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RUserId",
                table: "CheckMeActivityLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
