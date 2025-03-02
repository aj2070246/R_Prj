using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R.Database.Migrations
{
    /// <inheritdoc />
    public partial class t1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Age",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Age", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Captchas",
                columns: table => new
                {
                    CaptchaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CaptchaValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Captchas", x => x.CaptchaId);
                });

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
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthStatus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthStatus", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "LiveType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarriageStatus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarriageStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "UsersMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SenderUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserStatus = table.Column<int>(type: "int", nullable: false),
                    LastActivityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUserDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenderId = table.Column<long>(type: "bigint", nullable: false),
                    HealthStatusId = table.Column<long>(type: "bigint", nullable: false),
                    LiveTypeId = table.Column<long>(type: "bigint", nullable: false),
                    MarriageStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ProvinceId = table.Column<long>(type: "bigint", nullable: false),
                    IncomeAmountId = table.Column<long>(type: "bigint", nullable: true),
                    CarValueId = table.Column<long>(type: "bigint", nullable: true),
                    HomeValueId = table.Column<long>(type: "bigint", nullable: true),
                    RelationTypeId = table.Column<long>(type: "bigint", nullable: true),
                    Ghad = table.Column<int>(type: "int", nullable: false),
                    Vazn = table.Column<int>(type: "int", nullable: false),
                    RangePoost = table.Column<int>(type: "int", nullable: false),
                    CheildCount = table.Column<int>(type: "int", nullable: false),
                    FirstCheildAge = table.Column<int>(type: "int", nullable: false),
                    ZibaeeNumber = table.Column<int>(type: "int", nullable: false),
                    TipNUmber = table.Column<int>(type: "int", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileVerifyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileVerifyCodeExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MobileStatusId = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddressStatusId = table.Column<int>(type: "int", nullable: false),
                    EmailVerifyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailVerifyCodeExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_CarValue_CarValueId",
                        column: x => x.CarValueId,
                        principalTable: "CarValue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_HealthStatus_HealthStatusId",
                        column: x => x.HealthStatusId,
                        principalTable: "HealthStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_HomeValue_HomeValueId",
                        column: x => x.HomeValueId,
                        principalTable: "HomeValue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_IncomeAmount_IncomeAmountId",
                        column: x => x.IncomeAmountId,
                        principalTable: "IncomeAmount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_LiveType_LiveTypeId",
                        column: x => x.LiveTypeId,
                        principalTable: "LiveType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_MarriageStatus_MarriageStatusId",
                        column: x => x.MarriageStatusId,
                        principalTable: "MarriageStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_RelationType_RelationTypeId",
                        column: x => x.RelationTypeId,
                        principalTable: "RelationType",
                        principalColumn: "Id");
                });

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
                    RUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckMeActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckMeActivityLogs_Users_RUsersId",
                        column: x => x.RUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteDataLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FavoritedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Users_CarValueId",
                table: "Users",
                column: "CarValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HealthStatusId",
                table: "Users",
                column: "HealthStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HomeValueId",
                table: "Users",
                column: "HomeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IncomeAmountId",
                table: "Users",
                column: "IncomeAmountId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LiveTypeId",
                table: "Users",
                column: "LiveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MarriageStatusId",
                table: "Users",
                column: "MarriageStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProvinceId",
                table: "Users",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RelationTypeId",
                table: "Users",
                column: "RelationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Age");

            migrationBuilder.DropTable(
                name: "BlockedDataLog");

            migrationBuilder.DropTable(
                name: "Captchas");

            migrationBuilder.DropTable(
                name: "CheckMeActivityLogs");

            migrationBuilder.DropTable(
                name: "FavoriteDataLog");

            migrationBuilder.DropTable(
                name: "UsersMessages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CarValue");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "HealthStatus");

            migrationBuilder.DropTable(
                name: "HomeValue");

            migrationBuilder.DropTable(
                name: "IncomeAmount");

            migrationBuilder.DropTable(
                name: "LiveType");

            migrationBuilder.DropTable(
                name: "MarriageStatus");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "RelationType");
        }
    }
}
