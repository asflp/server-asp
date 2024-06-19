using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemWorkAsp.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 1, 5, 24, 26, 593, DateTimeKind.Utc).AddTicks(4037),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 7, 17, 50, 15, 459, DateTimeKind.Utc).AddTicks(5330));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 7, 17, 50, 15, 459, DateTimeKind.Utc).AddTicks(5330),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 6, 1, 5, 24, 26, 593, DateTimeKind.Utc).AddTicks(4037));
        }
    }
}
