using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthenticationOne.Migrations
{
    /// <inheritdoc />
    public partial class seedData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "companies",
                columns: new[] { "ID", "CreatedAt", "Description", "Name", "UpdatedAt", "type" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 9, 16, 16, 24, 39, 904, DateTimeKind.Local).AddTicks(8642), "Description for Company 1", "Company 1", new DateTime(2023, 9, 16, 16, 24, 39, 904, DateTimeKind.Local).AddTicks(8643), "Type A" },
                    { 2L, new DateTime(2023, 9, 16, 16, 24, 39, 904, DateTimeKind.Local).AddTicks(8647), "Description for Company 2", "Company 2", new DateTime(2023, 9, 16, 16, 24, 39, 904, DateTimeKind.Local).AddTicks(8648), "Type B" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "ID",
                keyValue: 2L);
        }
    }
}
