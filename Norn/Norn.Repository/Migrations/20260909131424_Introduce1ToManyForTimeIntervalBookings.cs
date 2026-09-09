using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Norn.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Introduce1ToManyForTimeIntervalBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_TimeIntervalId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "TimeIntervals");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TimeIntervalId",
                table: "Bookings",
                column: "TimeIntervalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_TimeIntervalId",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "TimeIntervals",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TimeIntervalId",
                table: "Bookings",
                column: "TimeIntervalId",
                unique: true);
        }
    }
}
