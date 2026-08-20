using ErpSystem.Contract.Purchasing.PurchaseOrders;
using FluentValidation;

namespace ErpSystem.Contract.Purchasing.Validators;

public class CreatePurchaseOrderValidator : AbstractValidator<CreatePurchaseOrder>
{
    public CreatePurchaseOrderValidator()
    {
        RuleFor(x => x.SupplierId)
            .NotEmpty().WithMessage("Supplier is required");

        RuleFor(x => x.OrderDate)
            .NotEmpty().WithMessage("Order date is required")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Order date cannot be in the future");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters");

        RuleFor(x => x.Details)
            .NotEmpty().WithMessage("At least one product is required");

        RuleForEach(x => x.Details)
            .SetValidator(new PurchaseOrderDetailValidator());
    }
}

public class PurchaseOrderDetailValidator : AbstractValidator<PurchaseOrderDetailDto>
{
    public PurchaseOrderDetailValidator()
    {
        RuleFor(x => x.ProductItemId)
            .NotEmpty().WithMessage("Product is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero");

        RuleFor(x => x.PurchasePrice)
            .GreaterThan(0).WithMessage("Purchase price must be greater than zero");
    }
}