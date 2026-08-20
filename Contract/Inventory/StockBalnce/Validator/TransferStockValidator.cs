namespace ErpSystem.Contract.Inventorey.StockBalnce.Validator
{
    public class TransferStockValidator : AbstractValidator<TransferStockRequest>
    {
        public TransferStockValidator()
        {
            RuleFor(x => x.ProductItemId)
                .NotEmpty().WithMessage("Product is required");

            RuleFor(x => x.SourceWarehouseId)
                .NotEmpty().WithMessage("Source warehouse is required");

            RuleFor(x => x.DestinationWarehouseId)
                .NotEmpty().WithMessage("Destination warehouse is required")
                .NotEqual(x => x.SourceWarehouseId).WithMessage("Source and destination warehouses must be different");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero")
                .LessThanOrEqualTo(999999999999999.999m).WithMessage("Quantity exceeds maximum allowed value")
                .Must(PrecisionScale).WithMessage("Quantity precision cannot exceed 3 decimal places");
        }

        private bool PrecisionScale(decimal quantity)
        {
            return quantity == Math.Round(quantity, 3);
        }
    }
}
