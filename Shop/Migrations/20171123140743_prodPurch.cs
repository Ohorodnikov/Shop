using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Shop.Migrations
{
    public partial class prodPurch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Purchase_PurchaseId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_PurchaseId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "ProductPurchase",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    PurchaseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPurchase", x => new { x.ProductId, x.PurchaseId });
                    table.ForeignKey(
                        name: "FK_ProductPurchase_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPurchase_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchase_PurchaseId",
                table: "ProductPurchase",
                column: "PurchaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPurchase");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_PurchaseId",
                table: "Product",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Purchase_PurchaseId",
                table: "Product",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
