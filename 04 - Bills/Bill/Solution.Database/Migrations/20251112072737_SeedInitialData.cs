using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Solution.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bill",
                columns: new[] { "Id", "BillNumber", "DateIssued" },
                values: new object[,]
                {
                    { 1, "BILL-2025-001", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "BILL-2025-002", new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "BILL-2025-003", new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "BillItem",
                columns: new[] { "Id", "BillId", "Name", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, "Laptop", 2, 150000 },
                    { 2, 1, "Mouse", 3, 5000 },
                    { 3, 2, "Monitor", 1, 80000 },
                    { 4, 2, "Keyboard", 2, 15000 },
                    { 5, 3, "Headset", 1, 25000 },
                    { 6, 3, "USB Cable", 5, 2000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BillItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BillItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BillItem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BillItem",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BillItem",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BillItem",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Bill",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bill",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bill",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
