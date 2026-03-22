using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixConcurrencyTokenV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2026, 5, 1, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2026, 5, 1, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2026, 5, 2, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2026, 5, 2, 18, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartTime",
                value: new DateTime(2026, 5, 3, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 6,
                column: "StartTime",
                value: new DateTime(2026, 5, 3, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 7,
                column: "StartTime",
                value: new DateTime(2026, 5, 4, 15, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 8,
                column: "StartTime",
                value: new DateTime(2026, 5, 4, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 9,
                column: "StartTime",
                value: new DateTime(2026, 5, 5, 22, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 10,
                column: "StartTime",
                value: new DateTime(2026, 5, 5, 17, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2025, 5, 1, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2025, 5, 1, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2025, 5, 2, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2025, 5, 2, 18, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartTime",
                value: new DateTime(2025, 5, 3, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 6,
                column: "StartTime",
                value: new DateTime(2025, 5, 3, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 7,
                column: "StartTime",
                value: new DateTime(2025, 5, 4, 15, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 8,
                column: "StartTime",
                value: new DateTime(2025, 5, 4, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 9,
                column: "StartTime",
                value: new DateTime(2025, 5, 5, 22, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 10,
                column: "StartTime",
                value: new DateTime(2025, 5, 5, 17, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
