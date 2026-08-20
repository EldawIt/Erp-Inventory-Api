namespace ErpSystem.Contract.Inventorey.Product.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProduct>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.NameArabic)
                .NotEmpty()
                .WithMessage("Arabic name is required.")
                .MaximumLength(50)
                .WithMessage("Arabic name must not exceed 50 characters.");

            RuleFor(x => x.NameEnglish)
                .NotEmpty()
                .WithMessage("English name is required.")
                .MaximumLength(50)
                .WithMessage("English name must not exceed 50 characters.");

            RuleFor(x => x.SellingPrice)
                .GreaterThan(0)
                .WithMessage("Selling price must be greater than zero.");
        }
    }
}