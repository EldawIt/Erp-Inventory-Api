namespace ErpSystem.Helpers
{
    public static class StockBalanceHelper
    {
       

        public static StockBalanceResponse ToResponse(this StockBalance stock)
        {
            return new StockBalanceResponse(
                stock.Id,
                stock.ProductItemId,
                stock.ProductItem?.NameArabic ?? "Unknown",
                stock.ProductItem?.InternalItemCode ?? "Unknown",
                stock.WarehouseId,
                stock.Warehouse?.WarehouseName ?? "Unknown",
                stock.RealQuantityOnShelf,
                stock.SafetyReorderLevel,
                stock.CurrentAverageCost,
                stock.TotalValue
            );
        }


        public static void ApplyDischarge(this StockBalance stock, decimal quantity)
        {
            stock.RealQuantityOnShelf -= quantity;
            stock.TotalValue = stock.RealQuantityOnShelf == 0
                ? 0
                : stock.RealQuantityOnShelf * stock.CurrentAverageCost;
        }


        public static void ApplyAddition(this StockBalance stock, decimal quantity, decimal unitPrice)
        {
            decimal incomingValue = quantity * unitPrice;
            decimal newQuantity = stock.RealQuantityOnShelf + quantity;
            decimal newTotalValue = stock.TotalValue + incomingValue;

            stock.CurrentAverageCost = newQuantity > 0 ? Math.Round(newTotalValue / newQuantity, 4) : 0;
            stock.RealQuantityOnShelf = newQuantity;
            stock.TotalValue = newTotalValue;
        }


        public static StockBalance CreateInitialStockBalance(Guid productId, Guid warehouseId)
        {
            return new StockBalance
            {
                ProductItemId = productId,
                WarehouseId = warehouseId,
                RealQuantityOnShelf = 0,
                CurrentAverageCost = 0,
                TotalValue = 0,
                SafetyReorderLevel = 0
            };
        }


        public static (InventoryTransaction Source, InventoryTransaction Dest) CreateTransferTransactions(
            TransferStockRequest request,
            decimal unitCost,
            decimal totalCost)
        {
            var source = new InventoryTransaction
            {
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Transfer,
                ProductItemId = request.ProductItemId,
                WarehouseId = request.SourceWarehouseId,
                Quantity = -request.Quantity,
                PriceDuringTransaction = unitCost,
                TotalCost = totalCost
            };

            var dest = new InventoryTransaction
            {
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Transfer,
                ProductItemId = request.ProductItemId,
                WarehouseId = request.DestinationWarehouseId,
                Quantity = request.Quantity,
                PriceDuringTransaction = unitCost,
                TotalCost = totalCost
            };

            return (source, dest);
        }


        public static InventoryTransaction CreateAdditionTransaction(ReceiveStockRequest request)
        {
            return new InventoryTransaction
            {
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Addition,
                ProductItemId = request.ProductItemId,
                WarehouseId = request.WarehouseId,
                Quantity = request.Quantity,
                PriceDuringTransaction = request.PurchasePrice,
                TotalCost = request.Quantity * request.PurchasePrice
            };
        }


        public static InventoryTransaction CreateDischargeTransaction(IssueStockRequest request,
            decimal averageCost)
        {
            return new InventoryTransaction
            {
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Discharge,
                ProductItemId = request.ProductItemId,
                WarehouseId = request.WarehouseId,
                Quantity = request.Quantity,
                PriceDuringTransaction = averageCost,
                TotalCost = request.Quantity * averageCost
            };
        }
        public static InventoryTransaction CreateTransaction(
            Guid productId,
            Guid warehouseId,
            DocumentDetailLine detail,
            decimal price,
            TransactionType type,
            Guid documentId)
        {
            var quantity = type == TransactionType.Discharge ? -detail.Quantity : detail.Quantity;

            return new InventoryTransaction
            {
                TransactionDate = DateTime.UtcNow,
                TransactionType = type,
                ProductItemId = productId,
                WarehouseId = warehouseId,
                Quantity = quantity,
                PriceDuringTransaction = price,
                TotalCost = Math.Round(Math.Abs(detail.Quantity) * price, 2),
                DocumentHeaderId = documentId,
                DocumentDetailLineId = detail.Id
            };
        }
    }
}
