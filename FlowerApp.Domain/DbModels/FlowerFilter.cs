namespace FlowerApp.Domain.DbModels;

public class FlowerFilter
{
    public DateTime? WateringFrequency { get; set; }
    
    public ToxicCategory[]? ToxicCategories { get; set; }

    public DateTime? TransplantFrequency { get; set; }
    
    public double? IlluminationInSuites { get; set; }
    public int? DurationInHours { get; set; }
}