using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConcurrencyToken", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[] { 9000, new Guid("c8072a63-0597-44f2-8151-505338a3d9df"), "admin@cinema.com", "Admin", "", "admin123", "+0000000000", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9000);
        }
    }
}
