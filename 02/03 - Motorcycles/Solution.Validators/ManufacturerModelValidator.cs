using Microsoft.AspNetCore.Http;

namespace Solution.Validators
{
    public class ManufacturerModelValidator : BaseValidator<ManufacturerModel>
    {
        public static string NameProperty => nameof(ManufacturerModel.Name);
        public static string GlobalProperty => "Global";

        public ManufacturerModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            if (IsPutMethod)
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required for update");
                //validalni hogy az id letezik
            }

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(64).WithMessage("Name cannot exceed 64 characters");
        }
    }
}