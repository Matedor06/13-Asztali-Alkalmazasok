using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solution.Services.Services.BillItem.Validators;

namespace Solution.Services.Services.Bill.Validators
{
    public class BillModelValidator : BaseValidator<BillModel>
    {
        public static string BillNumberProperty = nameof(BillModel.BillNumber);
        public static string DateIssuedProperty = nameof(BillModel.DateIssued);

        public BillModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            RuleFor(b => b.BillNumber)
                .NotEmpty().WithMessage($"{BillNumberProperty} is required.")
                .MaximumLength(50).WithMessage($"{BillNumberProperty} must not exceed 50 characters.");
            
            RuleFor(b => b.DateIssued)
                .LessThanOrEqualTo(DateTime.Now).WithMessage($"{DateIssuedProperty} cannot be in the future.");
            
            RuleFor(b => b.BillItems)
                .NotNull().WithMessage("BillItems cannot be null.")
                .NotEmpty()
                .WithMessage("A bill must have at least one bill item.");
            
            // Validáljuk a BillItems elemeit, ha van ilyen
            RuleForEach(b => b.BillItems)
                .SetValidator(new BillItemValidator(httpContextAccessor))
                .When(b => b.BillItems != null && b.BillItems.Any());
        }
    }
}
