namespace ErpSystem.Contract.Inventorey.StockBalnce
{
    public record TransferStockResponse(
    StockBalanceResponse SourceStock,
    StockBalanceResponse DestinationStock
);
}
