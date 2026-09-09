using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Norn.Repository.Migrations
{
    /// <inheritdoc />
    public partial class IncludeAutoForRoomOrg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingIds",
                table: "Users");

            migrationBuilder.AlterColumn<byte>(
                name: "Increment",
                table: "Rooms",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<int>>(
                name: "BookingIds",
                table: "Users",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Increment",
                table: "Rooms",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "smallint");
        }
    }
}
