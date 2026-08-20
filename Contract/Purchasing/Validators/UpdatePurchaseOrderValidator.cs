using ErpSystem.Contract.Purchasing.PurchaseOrders;
using FluentValidation;

namespace ErpSystem.Contract.Purchasing.Validators;

public class UpdatePurchaseOrderValidator : AbstractValidator<UpdatePurchaseOrder>
{
    public UpdatePurchaseOrderValidator()
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

        RuleFor(x => x.IsReceived)
            .NotNull().WithMessage("Received status is required");

        RuleForEach(x => x.Details)
            .SetValidator(new PurchaseOrderDetailValidator());
    }
}