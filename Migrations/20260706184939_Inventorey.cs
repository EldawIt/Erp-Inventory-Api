using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErpSystem.Migrations
{
    /// <inheritdoc />
    public partial class Inventorey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternalItemCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0.14m),
                    CalculationMethod = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WarehouseLocation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentHeaders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    SourceWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsPostedAndFrozen = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PostedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentHeaders_Warehouses_DestinationWarehouseId",
                        column: x => x.DestinationWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentHeaders_Warehouses_SourceWarehouseId",
                        column: x => x.SourceWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockBalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RealQuantityOnShelf = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    SafetyReorderLevel = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CurrentAverageCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatabaseVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockBalances_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockBalances_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentDetailLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    UserEnteredPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FrozenCalculatedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDetailLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentDetailLines_DocumentHeaders_DocumentHeaderId",
                        column: x => x.DocumentHeaderId,
                        principalTable: "DocumentHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentDetailLines_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FifoBatchQueues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceDocumentLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalIncomingQuantity = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    RemainingQuantity = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    BatchCostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FifoBatchQueues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FifoBatchQueues_DocumentDetailLines_SourceDocumentLineId",
                        column: x => x.SourceDocumentLineId,
                        principalTable: "DocumentDetailLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FifoBatchQueues_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FifoBatchQueues_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    DocumentDetailLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    PriceDuringTransaction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryTransactions_DocumentDetailLines_DocumentDetailLineId",
                        column: x => x.DocumentDetailLineId,
                        principalTable: "DocumentDetailLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransactions_DocumentHeaders_DocumentHeaderId",
                        column: x => x.DocumentHeaderId,
                        principalTable: "DocumentHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransactions_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransactions_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetailLines_DocumentHeaderId",
                table: "DocumentDetailLines",
                column: "DocumentHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetailLines_ProductItemId",
                table: "DocumentDetailLines",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentHeaders_DestinationWarehouseId",
                table: "DocumentHeaders",
                column: "DestinationWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentHeaders_DocumentNumber",
                table: "DocumentHeaders",
                column: "DocumentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentHeaders_SourceWarehouseId",
                table: "DocumentHeaders",
                column: "SourceWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_FifoBatchQueue_ArrivalDate",
                table: "FifoBatchQueues",
                column: "ArrivalDate");

            migrationBuilder.CreateIndex(
                name: "IX_FifoBatchQueue_Warehouse_Product",
                table: "FifoBatchQueues",
                columns: new[] { "WarehouseId", "ProductItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_FifoBatchQueues_ProductItemId",
                table: "FifoBatchQueues",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FifoBatchQueues_SourceDocumentLineId",
                table: "FifoBatchQueues",
                column: "SourceDocumentLineId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_Date",
                table: "InventoryTransactions",
                column: "TransactionDate");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_DocumentHeader",
                table: "InventoryTransactions",
                column: "DocumentHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_Warehouse_Product",
                table: "InventoryTransactions",
                columns: new[] { "WarehouseId", "ProductItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_DocumentDetailLineId",
                table: "InventoryTransactions",
                column: "DocumentDetailLineId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_ProductItemId",
                table: "InventoryTransactions",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_InternalItemCode",
                table: "ProductItems",
                column: "InternalItemCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_NameArabic",
                table: "ProductItems",
                column: "NameArabic");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_NameEnglish",
                table: "ProductItems",
                column: "NameEnglish");

            migrationBuilder.CreateIndex(
                name: "IX_StockBalances_ProductItemId",
                table: "StockBalances",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StockBalances_WarehouseId_ProductItemId",
                table: "StockBalances",
                columns: new[] { "WarehouseId", "ProductItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_WarehouseCode",
                table: "Warehouses",
                column: "WarehouseCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_WarehouseCode_IsDeleted",
                table: "Warehouses",
                columns: new[] { "WarehouseCode", "IsDeleted" },
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_WarehouseName",
                table: "Warehouses",
                column: "WarehouseName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FifoBatchQueues");

            migrationBuilder.DropTable(
                name: "InventoryTransactions");

            migrationBuilder.DropTable(
                name: "StockBalances");

            migrationBuilder.DropTable(
                name: "DocumentDetailLines");

            migrationBuilder.DropTable(
                name: "DocumentHeaders");

            migrationBuilder.DropTable(
                name: "ProductItems");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
