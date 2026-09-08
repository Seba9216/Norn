using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Norn.Repository.Migrations
{
    /// <inheritdoc />
    public partial class fixedRoomrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_OpenTimes_OpenTimeId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "OpenTimes");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_OpenTimeId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "OpenTimeId",
                table: "Rooms");

            migrationBuilder.AddColumn<bool>(
                name: "Friday",
                table: "Rooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "FromHour",
                table: "Rooms",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Monday",
                table: "Rooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Saturday",
                table: "Rooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sunday",
                table: "Rooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Thursday",
                table: "Rooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "ToHour",
                table: "Rooms",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Tuesday",
                table: "Rooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Wednesday",
                table: "Rooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friday",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "FromHour",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Monday",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Saturday",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Sunday",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Thursday",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ToHour",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Tuesday",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Wednesday",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "OpenTimeId",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OpenTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Friday = table.Column<bool>(type: "boolean", nullable: false),
                    FromHour = table.Column<byte>(type: "smallint", nullable: true),
                    Monday = table.Column<bool>(type: "boolean", nullable: false),
                    Saturday = table.Column<bool>(type: "boolean", nullable: false),
                    Sunday = table.Column<bool>(type: "boolean", nullable: false),
                    Thursday = table.Column<bool>(type: "boolean", nullable: false),
                    ToHour = table.Column<byte>(type: "smallint", nullable: true),
                    Tuesday = table.Column<bool>(type: "boolean", nullable: false),
                    Wednesday = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenTimes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_OpenTimeId",
                table: "Rooms",
                column: "OpenTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_OpenTimes_OpenTimeId",
                table: "Rooms",
                column: "OpenTimeId",
                principalTable: "OpenTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
