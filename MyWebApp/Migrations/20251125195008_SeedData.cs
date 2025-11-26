using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UsersTable",
                columns: new[] { "UserId", "IsOnline", "UserEmail", "UserName", "UserOrders", "UserPassword" },
                values: new object[,]
                {
                    { new Guid("bf9f6fbf-df81-45df-b037-82eaff7aa4d6"), false, "admin@gmail.com", "admin", null, "admin2627" },
                    { new Guid("f30379bd-ee19-4582-a4ed-976841d89390"), false, "mfurkantelli@gmail.com", "Furkan", null, "26278367" }
                });

            migrationBuilder.InsertData(
                table: "WatchesTable",
                columns: new[] { "Id", "Img", "Price", "WatchBrand", "WatchName" },
                values: new object[,]
                {
                    { new Guid("60c5951c-1c33-4184-8808-07e2a396d1dc"), "https://img.chrono24.com/images/uhren/42214924-sc9belurrolnws8n76a18bwe-ExtraLarge.jpg", 49999m, "Audemars Piguet", "26315ST Silver & Blue Dial Mens Watch Box Papers" },
                    { new Guid("843f7c4d-e699-491b-a3e9-045b51a25c70"), "https://d2j6dbq0eux0bg.cloudfront.net/images/38270005/4079824799.jpg", 12525m, "Rolex", "Silver Index Fluted Yellow Gold Leather 18238" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsersTable",
                keyColumn: "UserId",
                keyValue: new Guid("bf9f6fbf-df81-45df-b037-82eaff7aa4d6"));

            migrationBuilder.DeleteData(
                table: "UsersTable",
                keyColumn: "UserId",
                keyValue: new Guid("f30379bd-ee19-4582-a4ed-976841d89390"));

            migrationBuilder.DeleteData(
                table: "WatchesTable",
                keyColumn: "Id",
                keyValue: new Guid("60c5951c-1c33-4184-8808-07e2a396d1dc"));

            migrationBuilder.DeleteData(
                table: "WatchesTable",
                keyColumn: "Id",
                keyValue: new Guid("843f7c4d-e699-491b-a3e9-045b51a25c70"));
        }
    }
}
