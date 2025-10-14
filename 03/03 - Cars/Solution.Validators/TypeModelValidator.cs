using Microsoft.AspNetCore.Http;

namespace Solution.Validators
{
    public class TypeModelValidator : BaseValidator<TypeModel>
    {
        public static string NameProperty => nameof(TypeModel.Name);
        public static string GlobalProperty => "Global";

        public TypeModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            if(IsPutMethod)
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required for update");
            }

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(64).WithMessage("Name cannot exceed 64 characters");
        }
    }
}
