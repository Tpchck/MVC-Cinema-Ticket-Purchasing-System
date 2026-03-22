using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaTicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddConcurrencyAndSeedFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_ScreeningId",
                table: "Reservations");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Name", "Rows", "SeatsPerRow" },
                values: new object[,]
                {
                    { 4, "Grand Cinema Hall", 20, 30 },
                    { 5, "IMAX Laser", 18, 24 }
                });

            migrationBuilder.InsertData(
                table: "Screenings",
                columns: new[] { "Id", "CinemaId", "MovieTitle", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, "The Matrix", new DateTime(2025, 5, 1, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, "Free Guy", new DateTime(2025, 5, 1, 13, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 1, "The Green Mile", new DateTime(2025, 5, 2, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 2, "Fight Club", new DateTime(2025, 5, 2, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 2, "Total Recall", new DateTime(2025, 5, 3, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 3, "Spider-Man: Across the Spider-Verse", new DateTime(2025, 5, 5, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 4, "Inception", new DateTime(2025, 5, 3, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 5, "Interstellar", new DateTime(2025, 5, 4, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 5, "Dune: Part Two", new DateTime(2025, 5, 4, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 4, "Oppenheimer", new DateTime(2025, 5, 5, 22, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ScreeningId_RowPosition_SeatPosition",
                table: "Reservations",
                columns: new[] { "ScreeningId", "RowPosition", "SeatPosition" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_ScreeningId_RowPosition_SeatPosition",
                table: "Reservations");

            migrationBuilder.DeleteData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Screenings",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ScreeningId",
                table: "Reservations",
                column: "ScreeningId");
        }
    }
}
