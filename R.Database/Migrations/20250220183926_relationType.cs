using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R.Database.Migrations
{
    /// <inheritdoc />
    public partial class relationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RelationTypeId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RelationType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RelationTypeId",
                table: "Users",
                column: "RelationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RelationType_RelationTypeId",
                table: "Users",
                column: "RelationTypeId",
                principalTable: "RelationType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RelationType_RelationTypeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RelationType");

            migrationBuilder.DropIndex(
                name: "IX_Users_RelationTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RelationTypeId",
                table: "Users");
        }
    }
}
