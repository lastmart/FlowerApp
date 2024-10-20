using System.ComponentModel.DataAnnotations;
using FlowersCareAPI.Models;

public class FlowerFilter
{
    [Range(1, 365, ErrorMessage = "Watering frequency must be between 1 and 365 days.")]
    public int? WateringFrequency { get; set; }

    public LightParameters? LightRequirements { get; set; }

    public ToxicCategory[]? ToxicCategories { get; set; }

    [Range(1, 120, ErrorMessage = "Transplant frequency must be between 1 and 120 months.")]
    public int? TransplantFrequency { get; set; }
}