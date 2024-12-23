namespace FlowerApp.Domain.Models.FlowerModels;

public class FlowerFilterParams
{
    public WateringFrequency[]? WateringFrequency { get; set; }
    public ToxicCategory[]? ToxicCategories { get; set; }
    public Illumination[]? Illumination { get; set; }
}