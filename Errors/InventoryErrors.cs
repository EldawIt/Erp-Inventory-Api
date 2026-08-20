namespace ErpSystem.Errors
{
    public static class InventoryErrors
    {
        public static readonly Error WarehouseNotFound =
            Error.NotFound("Warehouse not found.");

        public static readonly Error WarehouseCodeAlreadyExists =
            Error.Conflict("Warehouse code already exists.");

        public static readonly Error WarehouseHasStockBalances =
            Error.Conflict("Cannot delete warehouse because it has associated stock balances.");

        public static readonly Error WarehouseIsInactive =
            Error.BadRequest("Warehouse is inactive.");

        public static readonly Error ProductNotFound =
            Error.NotFound("Product not found.");

        public static readonly Error ProductCodeAlreadyExists =
            Error.Conflict("Product code already exists.");

        public static readonly Error StockBalanceNotFound =
            Error.NotFound("Stock balance not found.");

        public static readonly Error StockBalanceAlreadyExists =
            Error.Conflict("Product already exists in this warehouse.");

        public static readonly Error InsufficientQuantity =
            Error.BadRequest("Insufficient quantity available.");

        public static readonly Error QuantityMustBeGreaterThanZero =
            Error.BadRequest("Quantity must be greater than zero.");

        public static readonly Error HasQuantityProduct =
            Error.BadRequest("Cannot execute request because stock is remaining in the warehouse.");
        public static readonly Error InvalidQuantity =
            Error.BadRequest("Quantity must be greater than zero");

        public static readonly Error InvalidPrice =
            Error.BadRequest("Price must be greater than or equal to zero");

        
    }
}