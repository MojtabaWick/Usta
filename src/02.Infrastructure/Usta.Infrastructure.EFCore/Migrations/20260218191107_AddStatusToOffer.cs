using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usta.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Offers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3c6e43dd-89b3-4314-b706-7b74871cd0e3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a5d43a31-9204-43fb-8866-25f114302ab6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "19d91e20-3b1f-44d2-a504-15adef2f9693");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagedUrl",
                value: "/Images\\Categories\\b22dbcc9-fbd1-4e79-8ac1-aa03b80ce90c.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagedUrl",
                value: "/Images\\Categories\\3c20c7c7-12e2-4645-8cda-2f73fd3e0fc0.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagedUrl",
                value: "/Images\\Categories\\d95c7625-89da-487a-9609-e19a632261e6.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagedUrl",
                value: "/Images\\Categories\\7d1c2910-21bd-497d-877c-fde0801e2d2e.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagedUrl",
                value: "/Images\\Categories\\feb876d8-ee5d-420c-bab6-7fbb32c3f917.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Offers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3b5f530c-3da4-4a78-9ecc-cc8cca2f02d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f91fe05b-8a36-4c86-b7c0-0ccc555ed7e0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3a0d0361-1ca5-441a-a540-62f99d50ef9a");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagedUrl",
                value: "/Image/Categories/kitchen.png");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagedUrl",
                value: "/Image/Categories/building.png");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagedUrl",
                value: "/Image/Categories/mobile.png");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagedUrl",
                value: "/Image/Categories/car.png");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagedUrl",
                value: "/Image/Categories/laptop.png");
        }
    }
}
