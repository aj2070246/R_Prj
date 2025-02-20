using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R.Database.Migrations
{
    /// <inheritdoc />
    public partial class poooool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CarValueId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "HomeValueId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IncomeAmountId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivityDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CarValue",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeValue",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeAmount",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeAmount", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CarValueId",
                table: "Users",
                column: "CarValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HomeValueId",
                table: "Users",
                column: "HomeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IncomeAmountId",
                table: "Users",
                column: "IncomeAmountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CarValue_CarValueId",
                table: "Users",
                column: "CarValueId",
                principalTable: "CarValue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_HomeValue_HomeValueId",
                table: "Users",
                column: "HomeValueId",
                principalTable: "HomeValue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_IncomeAmount_IncomeAmountId",
                table: "Users",
                column: "IncomeAmountId",
                principalTable: "IncomeAmount",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CarValue_CarValueId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_HomeValue_HomeValueId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_IncomeAmount_IncomeAmountId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CarValue");

            migrationBuilder.DropTable(
                name: "HomeValue");

            migrationBuilder.DropTable(
                name: "IncomeAmount");

            migrationBuilder.DropIndex(
                name: "IX_Users_CarValueId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HomeValueId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IncomeAmountId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CarValueId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HomeValueId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IncomeAmountId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastActivityDate",
                table: "Users");
        }
    }
}
