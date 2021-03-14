using Microsoft.EntityFrameworkCore.Migrations;

namespace MintGarage.Migrations
{
    public partial class ProductUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DescriptionProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DescriptionID",
                table: "Product",
                column: "DescriptionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Description_DescriptionID",
                table: "Product",
                column: "DescriptionID",
                principalTable: "Description",
                principalColumn: "DescriptionID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Description_DescriptionID",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_DescriptionID",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "DescriptionProduct",
                columns: table => new
                {
                    DescriptionID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionProduct", x => new { x.DescriptionID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_DescriptionProduct_Description_DescriptionID",
                        column: x => x.DescriptionID,
                        principalTable: "Description",
                        principalColumn: "DescriptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DescriptionProduct_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DescriptionProduct_ProductID",
                table: "DescriptionProduct",
                column: "ProductID");
        }
    }
}
