using ErpSystem.Contract.Inventorey.Warehouse;

namespace ErpSystem.Contract.Inventorey.Product.Validators
{
    public class WarehouseRequestValidator : AbstractValidator<WarehouseRequest>
    {
        public WarehouseRequestValidator()
        {
            RuleFor(x => x.WarehouseCode)
                .NotEmpty()
                .WithMessage("Warehouse code is required.")
                .MaximumLength(50).WithMessage("Warehouse code must not exceed 50 characters.")
                .Matches(@"^[a-zA-Z][a-zA-Z0-9-_]*$").WithMessage("Warehouse code must start with a letter.");

            RuleFor(x => x.WarehouseName)
                .NotEmpty().WithMessage("Warehouse name is required.")
                .MaximumLength(200).WithMessage("Warehouse name must not exceed 200 characters.");

            RuleFor(x => x.WarehouseLocation)
                .NotEmpty()
                .WithMessage("Warehouse location is required.")
                .MaximumLength(500).WithMessage("Warehouse location must not exceed 500 characters.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Activation status is required.");
        }
    }
}