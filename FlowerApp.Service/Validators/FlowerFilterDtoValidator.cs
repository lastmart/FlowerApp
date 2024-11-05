using FlowerApp.Domain.DbModels;
using FlowerApp.Domain.DTOModels;
using FluentValidation;

namespace FlowerApp.Service.Validators;

public class FlowerFilterDtoValidator: AbstractValidator<FlowerFilterDto>
{
    public FlowerFilterDtoValidator()
    {
        RuleFor(x => x.ToxicCategories)
            .Must(BeValidEnumValues)
            .WithMessage("Invalid Toxic Categories.")
            .When(x => x.ToxicCategories != null);

        RuleFor(x => x.WateringFrequency)
            .Must(BeAValidDate)
            .WithMessage("Invalid Watering Frequency.");

        RuleFor(x => x.TransplantFrequency)
            .Must(BeAValidDate)
            .WithMessage("Invalid Transplant Frequency.");

        RuleFor(x => x.IlluminationInSuites)
            .GreaterThan(0)
            .WithMessage("IlluminationInSuites must be greater than 0.");

        RuleFor(x => x.DurationInHours)
            .GreaterThan(0)
            .WithMessage("DurationInHours must be greater than 0.");
    }
    
    private bool BeValidEnumValues(string[] toxicCategories)
    {
        var validValues = Enum.GetNames(typeof(ToxicCategory)).ToList();

        return toxicCategories.All(tc => validValues.Contains(tc));
    }

    private bool BeAValidDate(DateTime? date)
    {
        return date == null || date >= DateTime.Today;
    }

}