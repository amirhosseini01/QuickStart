using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class ProductChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ProductStocks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialOffer",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductModels",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "ProductModels",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "ProductComments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ProductComments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "ProductCategories",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewOrder",
                table: "ProductCategories",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "ProductBrands",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewOrder",
                table: "ProductBrands",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ProductStocks");

            migrationBuilder.DropColumn(
                name: "IsSpecialOffer",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "ProductModels");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ViewOrder",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "ProductBrands");

            migrationBuilder.DropColumn(
                name: "ViewOrder",
                table: "ProductBrands");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "ProductModels",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
