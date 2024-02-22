using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductBrand_ProductBrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_ProductCategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductSeller_ProductSellerId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComment_AspNetUsers_UserId",
                table: "ProductComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComment_Product_ProductId",
                table: "ProductComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductModel_Product_ProductId",
                table: "ProductModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSeller_AspNetUsers_UserId",
                table: "ProductSeller");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStock_AspNetUsers_UserId",
                table: "ProductStock");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStock_ProductModel_ProductModelId",
                table: "ProductStock");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStock_Product_ProductId",
                table: "ProductStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStock",
                table: "ProductStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSeller",
                table: "ProductSeller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductModel",
                table: "ProductModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductComment",
                table: "ProductComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductBrand",
                table: "ProductBrand");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "ProductStock",
                newName: "ProductStocks");

            migrationBuilder.RenameTable(
                name: "ProductSeller",
                newName: "ProductSellers");

            migrationBuilder.RenameTable(
                name: "ProductModel",
                newName: "ProductModels");

            migrationBuilder.RenameTable(
                name: "ProductComment",
                newName: "ProductComments");

            migrationBuilder.RenameTable(
                name: "ProductCategory",
                newName: "ProductCategories");

            migrationBuilder.RenameTable(
                name: "ProductBrand",
                newName: "ProductBrands");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStock_UserId",
                table: "ProductStocks",
                newName: "IX_ProductStocks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStock_ProductModelId",
                table: "ProductStocks",
                newName: "IX_ProductStocks_ProductModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStock_ProductId",
                table: "ProductStocks",
                newName: "IX_ProductStocks_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSeller_UserId",
                table: "ProductSellers",
                newName: "IX_ProductSellers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductModel_ProductId",
                table: "ProductModels",
                newName: "IX_ProductModels_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComment_UserId",
                table: "ProductComments",
                newName: "IX_ProductComments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComment_ProductId",
                table: "ProductComments",
                newName: "IX_ProductComments_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductSellerId",
                table: "Products",
                newName: "IX_Products_ProductSellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductCategoryId",
                table: "Products",
                newName: "IX_Products_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductBrandId",
                table: "Products",
                newName: "IX_Products_ProductBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStocks",
                table: "ProductStocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSellers",
                table: "ProductSellers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductModels",
                table: "ProductModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductComments",
                table: "ProductComments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductBrands",
                table: "ProductBrands",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_Products_ProductId",
                table: "ProductComments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductModels_Products_ProductId",
                table: "ProductModels",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBrands_ProductBrandId",
                table: "Products",
                column: "ProductBrandId",
                principalTable: "ProductBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductSellers_ProductSellerId",
                table: "Products",
                column: "ProductSellerId",
                principalTable: "ProductSellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSellers_AspNetUsers_UserId",
                table: "ProductSellers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_AspNetUsers_UserId",
                table: "ProductStocks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_ProductModels_ProductModelId",
                table: "ProductStocks",
                column: "ProductModelId",
                principalTable: "ProductModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_Products_ProductId",
                table: "ProductStocks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_Products_ProductId",
                table: "ProductComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductModels_Products_ProductId",
                table: "ProductModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBrands_ProductBrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductSellers_ProductSellerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSellers_AspNetUsers_UserId",
                table: "ProductSellers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_AspNetUsers_UserId",
                table: "ProductStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_ProductModels_ProductModelId",
                table: "ProductStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_Products_ProductId",
                table: "ProductStocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStocks",
                table: "ProductStocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSellers",
                table: "ProductSellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductModels",
                table: "ProductModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductComments",
                table: "ProductComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductBrands",
                table: "ProductBrands");

            migrationBuilder.RenameTable(
                name: "ProductStocks",
                newName: "ProductStock");

            migrationBuilder.RenameTable(
                name: "ProductSellers",
                newName: "ProductSeller");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "ProductModels",
                newName: "ProductModel");

            migrationBuilder.RenameTable(
                name: "ProductComments",
                newName: "ProductComment");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "ProductCategory");

            migrationBuilder.RenameTable(
                name: "ProductBrands",
                newName: "ProductBrand");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStocks_UserId",
                table: "ProductStock",
                newName: "IX_ProductStock_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStocks_ProductModelId",
                table: "ProductStock",
                newName: "IX_ProductStock_ProductModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStocks_ProductId",
                table: "ProductStock",
                newName: "IX_ProductStock_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSellers_UserId",
                table: "ProductSeller",
                newName: "IX_ProductSeller_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductSellerId",
                table: "Product",
                newName: "IX_Product_ProductSellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Product",
                newName: "IX_Product_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductBrandId",
                table: "Product",
                newName: "IX_Product_ProductBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductModels_ProductId",
                table: "ProductModel",
                newName: "IX_ProductModel_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComment",
                newName: "IX_ProductComment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComments_ProductId",
                table: "ProductComment",
                newName: "IX_ProductComment_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStock",
                table: "ProductStock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSeller",
                table: "ProductSeller",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductModel",
                table: "ProductModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductComment",
                table: "ProductComment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductBrand",
                table: "ProductBrand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductBrand_ProductBrandId",
                table: "Product",
                column: "ProductBrandId",
                principalTable: "ProductBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategory_ProductCategoryId",
                table: "Product",
                column: "ProductCategoryId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductSeller_ProductSellerId",
                table: "Product",
                column: "ProductSellerId",
                principalTable: "ProductSeller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComment_AspNetUsers_UserId",
                table: "ProductComment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComment_Product_ProductId",
                table: "ProductComment",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductModel_Product_ProductId",
                table: "ProductModel",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSeller_AspNetUsers_UserId",
                table: "ProductSeller",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStock_AspNetUsers_UserId",
                table: "ProductStock",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStock_ProductModel_ProductModelId",
                table: "ProductStock",
                column: "ProductModelId",
                principalTable: "ProductModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStock_Product_ProductId",
                table: "ProductStock",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
