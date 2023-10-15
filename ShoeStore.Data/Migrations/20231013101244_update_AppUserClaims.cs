using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_AppUserClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "Role", "Admin", new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2181972b-18d9-45d9-bc84-6cfb29624c29", "AQAAAAIAAYagAAAAEEoNM7bD3e35PwWNHG/YYmL42STtU4ifOEgIjyTiX88bJ1dfeykgG+VRXaIIiDZc7A==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 10, 13, 17, 12, 44, 185, DateTimeKind.Local).AddTicks(4543));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 10, 13, 17, 12, 44, 185, DateTimeKind.Local).AddTicks(4559));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "99e4a934-cafa-4338-bcf2-2e2780e9ea25", "AQAAAAIAAYagAAAAENwRPJB1DWLMsqc1VAW5zaDQqLPHcIFpaB/FCJlg7LSS4Uv7IS1k2/UZijK8KL/tJA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 10, 8, 16, 40, 17, 853, DateTimeKind.Local).AddTicks(7309));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 10, 8, 16, 40, 17, 853, DateTimeKind.Local).AddTicks(7326));
        }
    }
}
