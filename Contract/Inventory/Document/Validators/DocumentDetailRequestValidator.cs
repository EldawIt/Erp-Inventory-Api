 

namespace ErpSystem.Contract.Inventory.Validators;

public class DocumentDetailRequestValidator : AbstractValidator<DocumentDetailRequest>
{
    public DocumentDetailRequestValidator()
    {
        RuleFor(x => x.ProductItemId)
            .NotEmpty().WithMessage("Product ID is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero");

        RuleFor(x => x.UserEnteredPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to zero");
    }
}