using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Norn.Repository.Migrations
{
    /// <inheritdoc />
    public partial class EnsureBookingStatusExists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookingStatuses",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { 1, "Awaiting" },
                    { 2, "Confirmed" },
                    { 3, "Cancelled" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookingStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookingStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookingStatuses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
