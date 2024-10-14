using System.ComponentModel.DataAnnotations;
public class FlowerFilter
{
    [Range(1, 365, ErrorMessage = "Watering frequency must be between 1 and 365 days.")]
    public int? WateringFrequency { get; set; }
    
    [EnumDataType(typeof(LightLevel), ErrorMessage = "Invalid light level.")]
    public LightLevel? LightRequirements { get; set; }
    public bool? IsToxic { get; set; }
    
    [Range(1, 120, ErrorMessage = "Transplant frequency must be between 1 and 120 months.")]
    public int? TransplantFrequency { get; set; }
}