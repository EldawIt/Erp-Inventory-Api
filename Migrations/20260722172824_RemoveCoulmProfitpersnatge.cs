using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErpSystem.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCoulmProfitpersnatge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfitPercentage",
                table: "ProductItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentAverageCost",
                table: "StockBalances",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "InventoryTransactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "InventoryTransactions");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentAverageCost",
                table: "StockBalances",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AddColumn<decimal>(
                name: "ProfitPercentage",
                table: "ProductItems",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0.30m);
        }
    }
}
