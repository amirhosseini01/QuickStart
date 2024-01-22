using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class Slider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProductStocks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProductComments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Image = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Thumbnail = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Link = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_UserId",
                table: "ProductStocks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_AspNetUsers_UserId",
                table: "ProductStocks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_AspNetUsers_UserId",
                table: "ProductStocks");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropIndex(
                name: "IX_ProductStocks_UserId",
                table: "ProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductStocks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductComments");
        }
    }
}
