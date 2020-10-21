using Microsoft.EntityFrameworkCore.Migrations;

namespace StopNShop2.Migrations
{
    public partial class ForeignKeyFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_Product_ProductId",
                table: "ProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Product_ProductId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_ProductId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ProductReview_ProductId",
                table: "ProductReview");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductReview");

            migrationBuilder.AddColumn<int>(
                name: "ProductFK",
                table: "ShoppingCart",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductFK",
                table: "ProductReview",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ProductFK",
                table: "ShoppingCart",
                column: "ProductFK");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductFK",
                table: "ProductReview",
                column: "ProductFK");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_Product_ProductFK",
                table: "ProductReview",
                column: "ProductFK",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Product_ProductFK",
                table: "ShoppingCart",
                column: "ProductFK",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_Product_ProductFK",
                table: "ProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Product_ProductFK",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_ProductFK",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ProductReview_ProductFK",
                table: "ProductReview");

            migrationBuilder.DropColumn(
                name: "ProductFK",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "ProductFK",
                table: "ProductReview");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ShoppingCart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductReview",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ProductId",
                table: "ShoppingCart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductId",
                table: "ProductReview",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_Product_ProductId",
                table: "ProductReview",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Product_ProductId",
                table: "ShoppingCart",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
