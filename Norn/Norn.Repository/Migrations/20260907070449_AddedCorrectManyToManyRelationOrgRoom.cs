using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Norn.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddedCorrectManyToManyRelationOrgRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationRoom_Organisations_OrganisationsId",
                table: "OrganisationRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationRoom_Rooms_RoomsId",
                table: "OrganisationRoom");

            migrationBuilder.DropColumn(
                name: "BookingIds",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "OrginisationIds",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "TimeIntervalIds",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomIds",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "BookingIds",
                table: "BookingStatuses");

            migrationBuilder.RenameColumn(
                name: "RoomsId",
                table: "OrganisationRoom",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "OrganisationsId",
                table: "OrganisationRoom",
                newName: "OrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationRoom_RoomsId",
                table: "OrganisationRoom",
                newName: "IX_OrganisationRoom_RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_Name",
                table: "Organisations",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationRoom_Organisations_OrganisationId",
                table: "OrganisationRoom",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationRoom_Rooms_RoomId",
                table: "OrganisationRoom",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationRoom_Organisations_OrganisationId",
                table: "OrganisationRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationRoom_Rooms_RoomId",
                table: "OrganisationRoom");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_Name",
                table: "Organisations");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "OrganisationRoom",
                newName: "RoomsId");

            migrationBuilder.RenameColumn(
                name: "OrganisationId",
                table: "OrganisationRoom",
                newName: "OrganisationsId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationRoom_RoomId",
                table: "OrganisationRoom",
                newName: "IX_OrganisationRoom_RoomsId");

            migrationBuilder.AddColumn<List<int>>(
                name: "BookingIds",
                table: "Rooms",
                type: "integer[]",
                nullable: true);

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

            migrationBuilder.AddColumn<List<int>>(
                name: "RoomIds",
                table: "Organisations",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "BookingIds",
                table: "BookingStatuses",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationRoom_Organisations_OrganisationsId",
                table: "OrganisationRoom",
                column: "OrganisationsId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationRoom_Rooms_RoomsId",
                table: "OrganisationRoom",
                column: "RoomsId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
