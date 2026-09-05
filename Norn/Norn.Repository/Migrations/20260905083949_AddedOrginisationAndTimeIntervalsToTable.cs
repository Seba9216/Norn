using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Norn.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrginisationAndTimeIntervalsToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeIntervalTypeId",
                table: "TimeIntervals",
                newName: "RoomId");

            migrationBuilder.AddColumn<List<int>>(
                name: "BookingIds",
                table: "Users",
                type: "integer[]",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "To",
                table: "TimeIntervals",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "From",
                table: "TimeIntervals",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "TimeIntervals",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "BookingIds",
                table: "Rooms",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Increment",
                table: "Rooms",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpenTimeId",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<List<int>>(
                name: "OrginisationIds",
                table: "Rooms",
                type: "integer[]",
                nullable: false);

            migrationBuilder.AddColumn<List<int>>(
                name: "TimeIntervalIds",
                table: "Rooms",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeLease",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookingStatusId1",
                table: "Bookings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OpenTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Monday = table.Column<bool>(type: "boolean", nullable: false),
                    Tuesday = table.Column<bool>(type: "boolean", nullable: false),
                    Wednesday = table.Column<bool>(type: "boolean", nullable: false),
                    Thursday = table.Column<bool>(type: "boolean", nullable: false),
                    Friday = table.Column<bool>(type: "boolean", nullable: false),
                    Saturday = table.Column<bool>(type: "boolean", nullable: false),
                    Sunday = table.Column<bool>(type: "boolean", nullable: false),
                    FromHour = table.Column<byte>(type: "smallint", nullable: true),
                    ToHour = table.Column<byte>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RoomIds = table.Column<List<int>>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationRoom",
                columns: table => new
                {
                    OrganisationsId = table.Column<int>(type: "integer", nullable: false),
                    RoomsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationRoom", x => new { x.OrganisationsId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_OrganisationRoom_Organisations_OrganisationsId",
                        column: x => x.OrganisationsId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationRoom_Rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeIntervals_RoomId",
                table: "TimeIntervals",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_OpenTimeId",
                table: "Rooms",
                column: "OpenTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingStatusId",
                table: "Bookings",
                column: "BookingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingStatusId1",
                table: "Bookings",
                column: "BookingStatusId1");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TimeIntervalId",
                table: "Bookings",
                column: "TimeIntervalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationRoom_RoomsId",
                table: "OrganisationRoom",
                column: "RoomsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingStatuses_BookingStatusId",
                table: "Bookings",
                column: "BookingStatusId",
                principalTable: "BookingStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingStatuses_BookingStatusId1",
                table: "Bookings",
                column: "BookingStatusId1",
                principalTable: "BookingStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TimeIntervals_TimeIntervalId",
                table: "Bookings",
                column: "TimeIntervalId",
                principalTable: "TimeIntervals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_OpenTimes_OpenTimeId",
                table: "Rooms",
                column: "OpenTimeId",
                principalTable: "OpenTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeIntervals_Rooms_RoomId",
                table: "TimeIntervals",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingStatuses_BookingStatusId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingStatuses_BookingStatusId1",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TimeIntervals_TimeIntervalId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_OpenTimes_OpenTimeId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeIntervals_Rooms_RoomId",
                table: "TimeIntervals");

            migrationBuilder.DropTable(
                name: "OpenTimes");

            migrationBuilder.DropTable(
                name: "OrganisationRoom");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TimeIntervals_RoomId",
                table: "TimeIntervals");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_OpenTimeId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleName",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookingStatusId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookingStatusId1",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TimeIntervalId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingIds",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "TimeIntervals");

            migrationBuilder.DropColumn(
                name: "BookingIds",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Increment",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "OpenTimeId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "OrginisationIds",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "TimeIntervalIds",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "TimeLease",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "BookingStatusId1",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "TimeIntervals",
                newName: "TimeIntervalTypeId");

            migrationBuilder.AlterColumn<long>(
                name: "To",
                table: "TimeIntervals",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<decimal>(
                name: "From",
                table: "TimeIntervals",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
