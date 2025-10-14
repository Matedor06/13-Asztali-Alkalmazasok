using Microsoft.AspNetCore.Http;

namespace Solution.Validators
{
    public class MotorcycleModelValidator: BaseValidator<MotorcycleModel>
    {

        public static string ModelProperty => nameof(MotorcycleModel.Model);
        public static string CubicProperty => nameof(MotorcycleModel.Cubic);
        public static string ManufacturerProperty => nameof(MotorcycleModel.Manufacturer);
        public static string TypeProperty => nameof(MotorcycleModel.Type);
        public static string NumberOfCylindersProperty => nameof(MotorcycleModel.NumberOfCylinders);
        public static string ReleaseYearProperty => nameof(MotorcycleModel.ReleaseYear);
        public static string GlobalProperty => "Global";




        public MotorcycleModelValidator(IHttpContextAccessor httpContextAccessor) :base(httpContextAccessor)
        {
            if(IsPutMethod)
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required for update");
                //validalni hogy az id letezik
            }

            RuleFor(x => x.Model).NotEmpty().WithMessage("Model is required");
            RuleFor(x => x.Cubic).NotNull().WithMessage("Cubic is required")
                                 .GreaterThan(0).WithMessage("Cubic has to be greater then 0");
            RuleFor(x => x.Manufacturer).NotNull().WithMessage("Manufacturer is required");
           
            RuleFor(x => x.Manufacturer.Id).GreaterThan(0).WithMessage("Manufacturer Id can not be 0");
            //validalni hogy letezik e ilyen id-jű gyártó
            RuleFor(x => x.NumberOfCylinders).NotNull().WithMessage("Cylinders are required")
                                             .GreaterThan(0).WithMessage("Number of cylynders has to be greter then 0");
            RuleFor(x => x.ReleaseYear).NotNull().WithMessage("Release year is required")
                                       .InclusiveBetween(1900, DateTime.Now.Year).WithMessage("Invalid release year");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required");
            //validalni hogy letezik e ilyen id-jű típus
            RuleFor(x => x.Type.Id).GreaterThan(0).WithMessage("Type Id can not be 0");

        }

    }
}
