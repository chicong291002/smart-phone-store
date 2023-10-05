using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_code_new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dob",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AppUsers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AppUsers",
                newName: "Address");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "Address", "ConcurrencyStamp", "Name", "PasswordHash" },
                values: new object[] { "Ca Mau", "6ed14f78-4207-467f-9b0d-77447198f80c", "Chi Cong", "AQAAAAIAAYagAAAAEMuYYaxEcJdGBx35hyIieiB8h/b+r1v+ZOGW87Xzw8Ef6ZLiPM0NIpkPslm3hYAy3A==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 10, 6, 1, 0, 21, 11, DateTimeKind.Local).AddTicks(3600));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 10, 6, 1, 0, 21, 11, DateTimeKind.Local).AddTicks(3616));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AppUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "AppUsers",
                newName: "FirstName");

            migrationBuilder.AddColumn<DateTime>(
                name: "Dob",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "Dob", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "42482556-27b3-489b-bd20-b4b14fc3ac81", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chi", "Cong", "AQAAAAIAAYagAAAAEB2abP2Xhsg0hkd9OgSrs1HSbGTg1rVerdGt1f24lcAfZU8d5NDLz3FUXAue0ESWHg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 10, 4, 10, 59, 43, 552, DateTimeKind.Local).AddTicks(1606));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 10, 4, 10, 59, 43, 552, DateTimeKind.Local).AddTicks(1619));
        }
    }
}
