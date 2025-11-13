using Solution.Services.Services.Bill.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Services.Services.BillItem.Validators;

public class BillItemValidator : BaseValidator<BillItemModel>
{
    public static string NameProperty = nameof(BillItemModel.Name);
    public static string UnitPriceProperty = nameof(BillItemModel.UnitPrice);
    public static string QuantityProperty = nameof(BillItemModel.Quantity);
    public static string BillIdProperty = nameof(BillItemModel.BillId);

    public BillItemValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        RuleFor(bi => bi.Name)
            .NotEmpty().WithMessage($"{NameProperty} is required.")
            .MaximumLength(100).WithMessage($"{NameProperty} must not exceed 100 characters.");
        
        RuleFor(bi => bi.UnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage($"{UnitPriceProperty} must be non-negative.");
        
        RuleFor(bi => bi.Quantity)
            .GreaterThan(0).WithMessage($"{QuantityProperty} must be greater than zero.");
        
        // BillId validáció csak önálló BillItem POST kéréseknél szükséges
        // Bill létrehozáskor a tételek BillId-ja automatikusan beállításra kerül
        When(bi => !IsPutMethod && httpContextAccessor?.HttpContext?.Request?.Path.Value?.Contains("/billitem", StringComparison.OrdinalIgnoreCase) == true, () =>
        {
            RuleFor(bi => bi.BillId)
                .GreaterThan(0).WithMessage($"{BillIdProperty} must be greater than zero.");
        });
    }
}
