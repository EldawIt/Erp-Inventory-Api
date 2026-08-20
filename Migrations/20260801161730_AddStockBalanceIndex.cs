using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErpSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddStockBalanceIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockBalances_ProductItemId",
                table: "StockBalances");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "DocumentDetailLines",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_StockBalance_ProductItem_Warehouse",
                table: "StockBalances",
                columns: new[] { "ProductItemId", "WarehouseId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockBalance_ProductItem_Warehouse",
                table: "StockBalances");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "DocumentDetailLines");

            migrationBuilder.CreateIndex(
                name: "IX_StockBalances_ProductItemId",
                table: "StockBalances",
                column: "ProductItemId");
        }
    }
}
