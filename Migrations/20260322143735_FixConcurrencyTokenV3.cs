using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixConcurrencyTokenV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "IMAX Auditoria");

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "IMAX VIP Auditoria");

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Grand Cinema Auditoria 5D");

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "IMAX Laser Auditoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Room 1");

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Room 2 (VIP)");

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Room 3 (5D VIP)");

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "IMAX Laser");
        }
    }
}
