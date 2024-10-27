using System.ComponentModel.DataAnnotations;
using FlowerApp.Domain.DbModels;

namespace FlowerApp.Domain.DTOModels;

public class FlowerFilterDto
{
    public DateTime? WateringFrequency { get; set; }
    
    [EnumValidation(typeof(ToxicCategory))]
    public string[]? ToxicCategories { get; set; }

    public DateTime? TransplantFrequency { get; set; }
    
    public double? IlluminationInSuites { get; set; }
    public int? DurationInHours { get; set; }
}

public class EnumValidationAttribute : ValidationAttribute
{
    private readonly Type _enumType;

    public EnumValidationAttribute(Type enumType)
    {
        _enumType = enumType;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;

        var stringArray = value as string[];
        if (stringArray == null) return ValidationResult.Success;

        var validValues = Enum.GetNames(_enumType);
        var invalidValues = stringArray.Where(v => !validValues.Contains(v)).ToList();

        if (invalidValues.Any())
        {
            return new ValidationResult(
                $"Invalid values: {string.Join(", ", invalidValues)}. " +
                $"Valid values are: {string.Join(", ", validValues)}");
        }

        return ValidationResult.Success;
    }
}