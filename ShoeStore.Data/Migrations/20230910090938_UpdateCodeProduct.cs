using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCodeProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5b7291d7-67f0-47ca-a88e-54f51ddc1e7b", "AQAAAAIAAYagAAAAEGZ1K8ngSCK5ARNHtkFDw6xIwbxX9msqWWgR4xlxFF5XKeYBiGCjpqJRIrktIt3I/g==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2023, 9, 10, 16, 9, 38, 608, DateTimeKind.Local).AddTicks(87), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "Stock" },
                values: new object[] { new DateTime(2023, 9, 10, 16, 9, 38, 608, DateTimeKind.Local).AddTicks(103), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f299560b-c6e8-47e0-9e92-8256c3115752", "AQAAAAIAAYagAAAAEFh4SZpNfRo8G0Z1QXs66mGWGHDKGDJ3Oe+XkJbO51F1ycccC1dZK5tB6XQ+dBmOcQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 8, 31, 22, 1, 15, 261, DateTimeKind.Local).AddTicks(448));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 8, 31, 22, 1, 15, 261, DateTimeKind.Local).AddTicks(460));
        }
    }
}
