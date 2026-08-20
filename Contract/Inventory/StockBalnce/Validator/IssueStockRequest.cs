namespace ErpSystem.Contract.Inventorey.StockBalnce.Validator
{
    public class IssueStockValidator : AbstractValidator<IssueStockRequest>
    {
        public IssueStockValidator()
        {
            RuleFor(x => x.ProductItemId)
                 .NotEmpty().WithMessage("Product is required");

            RuleFor(x => x.WarehouseId)
                .NotEmpty().WithMessage("Warehouse is required");

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
