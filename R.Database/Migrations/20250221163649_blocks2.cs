using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R.Database.Migrations
{
    /// <inheritdoc />
    public partial class blocks2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockedDataLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlockedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RUsersId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedDataLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockedDataLog_Users_RUsersId",
                        column: x => x.RUsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CheckMeActivityLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId_CheckedMe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RUsersId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckMeActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckMeActivityLogs_Users_RUsersId",
                        column: x => x.RUsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteDataLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlockedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RUsersId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteDataLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteDataLog_Users_RUsersId",
                        column: x => x.RUsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockedDataLog_RUsersId",
                table: "BlockedDataLog",
                column: "RUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckMeActivityLogs_RUsersId",
                table: "CheckMeActivityLogs",
                column: "RUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteDataLog_RUsersId",
                table: "FavoriteDataLog",
                column: "RUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockedDataLog");

            migrationBuilder.DropTable(
                name: "CheckMeActivityLogs");

            migrationBuilder.DropTable(
                name: "FavoriteDataLog");
        }
    }
}
