using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9565af4a-77f9-49cc-8220-de2b9275b009", "AQAAAAIAAYagAAAAEA7jZrqAbvFvqmljtTEQhARBSTIxIuLc6UUCT5/pVt21HudrTgWjYngnRrDdQUivtg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 14, 20, 26, 15, 549, DateTimeKind.Local).AddTicks(3954));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 9, 14, 20, 26, 15, 549, DateTimeKind.Local).AddTicks(3969));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a4d18489-5ebf-40e5-8e44-f719c7961e0b", "AQAAAAIAAYagAAAAEJ3J1jYVTB/9s0+iLPrvo2hWxvOOAwrnlO0vmtD2tb0WEs6cMAM1BiJQjgsQjaO7Ww==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 1, 13, 83, DateTimeKind.Local).AddTicks(5765));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 9, 11, 22, 1, 13, 83, DateTimeKind.Local).AddTicks(5787));
        }
    }
}
