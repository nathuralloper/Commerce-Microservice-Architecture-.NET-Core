using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Persistence.Database.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Catalog",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                schema: "Catalog",
                columns: table => new
                {
                    ProductInStockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Stock = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ProductInStockId);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Catalog",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Description 1", "Product 1", 405m },
                    { 2, "Description 2", "Product 2", 718m },
                    { 3, "Description 3", "Product 3", 275m },
                    { 4, "Description 4", "Product 4", 907m },
                    { 5, "Description 5", "Product 5", 634m },
                    { 6, "Description 6", "Product 6", 336m },
                    { 7, "Description 7", "Product 7", 418m },
                    { 8, "Description 8", "Product 8", 482m },
                    { 9, "Description 9", "Product 9", 384m },
                    { 10, "Description 10", "Product 10", 775m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 36.0 },
                    { 2, 2, 35.0 },
                    { 3, 3, 1.0 },
                    { 4, 4, 7.0 },
                    { 5, 5, 34.0 },
                    { 6, 6, 4.0 },
                    { 7, 7, 14.0 },
                    { 8, 8, 22.0 },
                    { 9, 9, 41.0 },
                    { 10, 10, 35.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                schema: "Catalog",
                table: "Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                schema: "Catalog",
                table: "Stocks",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductInStockId",
                schema: "Catalog",
                table: "Stocks",
                column: "ProductInStockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Catalog");
        }
    }
}
