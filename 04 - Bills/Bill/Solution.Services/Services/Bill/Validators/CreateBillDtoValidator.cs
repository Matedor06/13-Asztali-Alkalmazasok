using Solution.Services.Services.Bill.Dtos;

namespace Solution.Services.Services.Bill.Validators;

public class CreateBillDtoValidator : AbstractValidator<CreateBillDto>
{
    public static string BillNumberProperty = nameof(CreateBillDto.BillNumber);
    public static string DateIssuedProperty = nameof(CreateBillDto.DateIssued);

    public CreateBillDtoValidator()
    {
        RuleFor(b => b.BillNumber)
            .NotEmpty().WithMessage($"{BillNumberProperty} is required.")
            .MaximumLength(50).WithMessage($"{BillNumberProperty} must not exceed 50 characters.");
        
        RuleFor(b => b.DateIssued)
            .LessThanOrEqualTo(DateTime.Now).WithMessage($"{DateIssuedProperty} cannot be in the future.");
        
        RuleFor(b => b.BillItems)
            .NotNull().WithMessage("BillItems cannot be null.");
        
        // Validáljuk a BillItems elemeit, ha van ilyen
        RuleForEach(b => b.BillItems)
            .SetValidator(new CreateBillItemDtoValidator())
            .When(b => b.BillItems != null && b.BillItems.Any());
    }
}

public class CreateBillItemDtoValidator : AbstractValidator<CreateBillItemDto>
{
    public static string NameProperty = nameof(CreateBillItemDto.Name);
    public static string UnitPriceProperty = nameof(CreateBillItemDto.UnitPrice);
    public static string QuantityProperty = nameof(CreateBillItemDto.Quantity);

    public CreateBillItemDtoValidator()
    {
        RuleFor(bi => bi.Name)
            .NotEmpty().WithMessage($"{NameProperty} is required.")
            .MaximumLength(100).WithMessage($"{NameProperty} must not exceed 100 characters.");
        
        RuleFor(bi => bi.UnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage($"{UnitPriceProperty} must be non-negative.");
        
        RuleFor(bi => bi.Quantity)
            .GreaterThan(0).WithMessage($"{QuantityProperty} must be greater than zero.");
    }
}
