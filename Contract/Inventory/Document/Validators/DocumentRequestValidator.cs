using ErpSystem.Contract.Inventory.Validators;

namespace ErpSystem.Contract.Inventory.Document.Validators
{
    public class DocumentRequestValidator : AbstractValidator<DocumentRequest>
    {
        public DocumentRequestValidator()
        {
            RuleFor(x => x.DocumentDate)
                .NotEmpty().WithMessage("Document date is required")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Document date cannot be in the future");

            RuleFor(x => x.TransactionType)
                .IsInEnum().WithMessage("Invalid transaction type");

            RuleFor(x => x.Details)
                .NotEmpty().WithMessage("Document must have at least one detail line")
                .Must(details => details.Count > 0).WithMessage("Document must have at least one detail line");

            RuleForEach(x => x.Details)
                .SetValidator(new DocumentDetailRequestValidator());

            When(x => x.TransactionType == TransactionType.Discharge, () =>
            {
                RuleFor(x => x.SourceWarehouseId)
                    .NotEmpty().WithMessage("Source warehouse is required for discharge");
            });

            When(x => x.TransactionType == TransactionType.Addition, () =>
            {
                RuleFor(x => x.DestinationWarehouseId)
                    .NotEmpty().WithMessage("Destination warehouse is required for addition");
            });

            When(x => x.TransactionType == TransactionType.Transfer, () =>
            {
                RuleFor(x => x.SourceWarehouseId)
                    .NotEmpty().WithMessage("Source warehouse is required for transfer");

                RuleFor(x => x.DestinationWarehouseId)
                    .NotEmpty().WithMessage("Destination warehouse is required for transfer")
                    .NotEqual(x => x.SourceWarehouseId).WithMessage("Source and destination warehouses cannot be the same");
            });

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes must not exceed 500 characters");
        }
    }
}
