using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdersTable",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    WatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WatchBrand = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersTable", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "UsersTable",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTable", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "WatchesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WatchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WatchBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchesTable", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UsersTable",
                columns: new[] { "UserId", "IsOnline", "UserEmail", "UserName", "UserPassword" },
                values: new object[,]
                {
                    { new Guid("16e762fa-6db5-40fe-9f94-049f475afc01"), false, "random@gmail.com", "Random", "random1234" },
                    { new Guid("bf9f6fbf-df81-45df-b037-82eaff7aa4d6"), false, "admin@gmail.com", "admin", "admin2627" }
                });

            migrationBuilder.InsertData(
                table: "WatchesTable",
                columns: new[] { "Id", "Img", "Price", "WatchBrand", "WatchName" },
                values: new object[,]
                {
                    { new Guid("01706492-d963-4c7f-a069-9d80f42e203f"), "https://www.omegawatches.com/media/catalog/product/o/m/omega-seamaster-diver-300m-co-axial-master-chronometer-42-mm-21032422001005-2b286f.png?w=700", 337m, "Omega", "Seamaster Diver 300M" },
                    { new Guid("2e0592bd-a5a4-485e-992d-9985ac0e3b57"), "https://img.chrono24.com/images/uhren/43258204-pk3j42yayatax2wodailsezm-ExtraLarge.jpg", 15999m, "A. Lange & Söhne", "Thin" },
                    { new Guid("3f2b06b8-aebb-4866-a194-c93bd381f11b"), "https://cdn.saatvesaat.com.tr/media/catalog/product/b/5/b5a02869a055070e6ed199509d879f19a4dd08cbeb488683f231fe5148fda3e0.jpeg", 8770m, "WELDER", "Welder WRC407 45 mm Erkek Kol Saati" },
                    { new Guid("58d1f733-7a74-4ec8-afcc-d67139db2846"), "https://i.ebayimg.com/images/g/IwEAAeSwQaJoyv-a/s-l1200.jpg", 6895m, "Cartier", "De Cartier Large Silver Dial Stainless Steel WSSA0018 4072" },
                    { new Guid("5a2ec16c-d2a3-4023-a8bc-aaf12f6b0017"), "https://img.chrono24.com/images/uhren/42798797-ultc6577p1gbjwgwgy79ogrh-ExtraLarge.jpg", 2450m, "Ernest Borel", "Triple Calendar Moon Phase Indicates week day month date and accurate moon phase" },
                    { new Guid("60c5951c-1c33-4184-8808-07e2a396d1dc"), "https://img.chrono24.com/images/uhren/42214924-sc9belurrolnws8n76a18bwe-ExtraLarge.jpg", 49999m, "Audemars Piguet", "26315ST Silver & Blue Dial Mens Watch Box Papers" },
                    { new Guid("843f7c4d-e699-491b-a3e9-045b51a25c70"), "https://d2j6dbq0eux0bg.cloudfront.net/images/38270005/4079824799.jpg", 12525m, "Rolex", "Silver Index Fluted Yellow Gold Leather 18238" },
                    { new Guid("b720e0ed-5b5d-4aaf-97dd-5d6defb83b1a"), "https://s3.us-east-1.amazonaws.com/ISHOWIMAGES/ROLEX+V7/wp/model_gallery_assets_portrait/Slide4_portrait/m126506-0001.webp", 28000m, "Rolex", "Sport Daytona 16518 Full set Pre-owned 1997" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersTable");

            migrationBuilder.DropTable(
                name: "UsersTable");

            migrationBuilder.DropTable(
                name: "WatchesTable");
        }
    }
}
