namespace ErpSystem.Helpers;

public interface ICostCalculator
{
    decimal CalculateWeightedAverage(decimal oldQuantity, decimal oldCost, decimal newQuantity, decimal newPrice);
    decimal CalculateProfitMargin(decimal sellingPrice, decimal averageCost);
    decimal CalculateMarkupPercentage(decimal sellingPrice, decimal averageCost);
    decimal CalculateTotalValue(decimal quantity, decimal cost);
}

public class CostCalculator : ICostCalculator
{
    public decimal CalculateWeightedAverage(
        decimal oldQuantity,
        decimal oldCost,
        decimal newQuantity,
        decimal newPrice)
    {
        var totalQty = oldQuantity + newQuantity;

        if (totalQty <= 0)
            return 0;

        var totalCost = (oldQuantity * oldCost) + (newQuantity * newPrice);

        return Math.Round(totalCost / totalQty, 4);
    }

    public decimal CalculateProfitMargin(decimal sellingPrice, decimal averageCost)
    {
        if (sellingPrice <= 0)
            return 0;

        return Math.Round(((sellingPrice - averageCost) / sellingPrice) * 100, 2);
    }

    public decimal CalculateMarkupPercentage(decimal sellingPrice, decimal averageCost)
    {
        if (averageCost <= 0)
            return 0;

        return Math.Round(((sellingPrice - averageCost) / averageCost) * 100, 2);
    }

    public decimal CalculateTotalValue(decimal quantity, decimal cost)
    {
        if (quantity <= 0 || cost <= 0)
            return 0;

        return Math.Round(quantity * cost, 2);
    }
}