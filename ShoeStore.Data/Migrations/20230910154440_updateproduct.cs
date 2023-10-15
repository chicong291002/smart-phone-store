using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a4549779-441a-48e6-b022-6642f6a64718", "AQAAAAIAAYagAAAAEJJEnDdNjjOkvKkl2gSvKiSygkdVCJhKJhUUR58JMq463xNT+pfKyvVulrpSwy4LOQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 10, 22, 44, 40, 785, DateTimeKind.Local).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 9, 10, 22, 44, 40, 785, DateTimeKind.Local).AddTicks(3879));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

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
                columns: new[] { "DateCreated", "Thumbnail" },
                values: new object[] { new DateTime(2023, 9, 10, 16, 9, 38, 608, DateTimeKind.Local).AddTicks(87), "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "Thumbnail" },
                values: new object[] { new DateTime(2023, 9, 10, 16, 9, 38, 608, DateTimeKind.Local).AddTicks(103), "" });
        }
    }
}
