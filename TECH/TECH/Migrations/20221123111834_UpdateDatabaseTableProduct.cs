using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TECH.Migrations
{
    public partial class UpdateDatabaseTableProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "commodities",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "insurance",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "manufacturer_id",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "percent_price",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "promotion",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "priceimprot",
                table: "product_quantity",
                type: "decimal(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "pricesell",
                table: "product_quantity",
                type: "decimal(18,0)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "color_images",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_quantities_id = table.Column<int>(type: "int", nullable: true),
                    image_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_color_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_color_images_images_image_id",
                        column: x => x.image_id,
                        principalTable: "images",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_color_images_product_quantity_product_quantities_id",
                        column: x => x.product_quantities_id,
                        principalTable: "product_quantity",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "manufacturer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    email = table.Column<string>(type: "varchar(200)", nullable: true),
                    phone = table.Column<string>(type: "varchar(20)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufacturer", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_manufacturer_id",
                table: "products",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "IX_color_images_image_id",
                table: "color_images",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_color_images_product_quantities_id",
                table: "color_images",
                column: "product_quantities_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_manufacturer_manufacturer_id",
                table: "products",
                column: "manufacturer_id",
                principalTable: "manufacturer",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_manufacturer_manufacturer_id",
                table: "products");

            migrationBuilder.DropTable(
                name: "color_images");

            migrationBuilder.DropTable(
                name: "manufacturer");

            migrationBuilder.DropIndex(
                name: "IX_products_manufacturer_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "commodities",
                table: "products");

            migrationBuilder.DropColumn(
                name: "insurance",
                table: "products");

            migrationBuilder.DropColumn(
                name: "manufacturer_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "percent_price",
                table: "products");

            migrationBuilder.DropColumn(
                name: "promotion",
                table: "products");

            migrationBuilder.DropColumn(
                name: "priceimprot",
                table: "product_quantity");

            migrationBuilder.DropColumn(
                name: "pricesell",
                table: "product_quantity");
        }
    }
}
