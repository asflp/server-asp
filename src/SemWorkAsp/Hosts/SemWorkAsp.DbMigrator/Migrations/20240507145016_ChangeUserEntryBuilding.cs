using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemWorkAsp.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserEntryBuilding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HomeNumber",
                table: "advertisements",
                newName: "Building");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Building",
                table: "advertisements",
                newName: "HomeNumber");
        }
    }
}
