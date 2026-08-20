using ErpSystem.Contract.Purchasing.Suppliers;
using FluentValidation;

namespace ErpSystem.Contract.Purchasing.Validators;

public class UpdateSupplierValidator : AbstractValidator<UpdateSupplier>
{
    public UpdateSupplierValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Supplier name is required")
            .MaximumLength(200).WithMessage("Supplier name must not exceed 200 characters");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required")
            .MaximumLength(20).WithMessage("Phone number must not exceed 20 digits")
            .Matches(@"^[0-9]+$").WithMessage("Phone number must contain only digits");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email address")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Address)
            .MaximumLength(500).WithMessage("Address must not exceed 500 characters");

        RuleFor(x => x.TaxNumber)
            .MaximumLength(50).WithMessage("Tax number must not exceed 50 characters");

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("Active status is required");
    }
}