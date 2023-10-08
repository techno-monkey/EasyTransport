using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ET.Trans.Bus.Migrations
{
    /// <inheritdoc />
    public partial class bus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransporterName",
                table: "Buses",
                newName: "CreatedBy");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Buses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "TransporterId",
                table: "Buses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "TransporterId",
                table: "Buses");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Buses",
                newName: "TransporterName");
        }
    }
}
