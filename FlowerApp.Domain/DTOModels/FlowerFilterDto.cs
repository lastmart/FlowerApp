using System.ComponentModel.DataAnnotations;
using FlowerApp.Domain.DbModels;

namespace FlowerApp.Domain.DTOModels;

public class FlowerFilterDto
{
    public DateTime? WateringFrequency { get; set; }
    
    public string[]? ToxicCategories { get; set; }

    public DateTime? TransplantFrequency { get; set; }
    
    public double? IlluminationInSuites { get; set; }
    public int? DurationInHours { get; set; }
}