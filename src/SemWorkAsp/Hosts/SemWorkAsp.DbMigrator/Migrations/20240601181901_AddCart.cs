using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemWorkAsp.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class AddCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 1, 21, 19, 0, 824, DateTimeKind.Utc).AddTicks(257),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 7, 17, 50, 15, 459, DateTimeKind.Utc).AddTicks(5330));

            migrationBuilder.CreateTable(
                name: "AdvertisementUser",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uuid", nullable: false),
                    InCartId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementUser", x => new { x.CartId, x.InCartId });
                    table.ForeignKey(
                        name: "FK_AdvertisementUser_advertisements_CartId",
                        column: x => x.CartId,
                        principalTable: "advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertisementUser_users_InCartId",
                        column: x => x.InCartId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementUser_InCartId",
                table: "AdvertisementUser",
                column: "InCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertisementUser");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 7, 17, 50, 15, 459, DateTimeKind.Utc).AddTicks(5330),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 6, 1, 21, 19, 0, 824, DateTimeKind.Utc).AddTicks(257));
        }
    }
}
