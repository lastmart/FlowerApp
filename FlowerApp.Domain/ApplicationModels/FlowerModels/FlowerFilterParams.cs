using FlowerApp.Domain.DbModels;

namespace FlowerApp.Domain.ApplicationModels.FlowerModels;

public class FlowerFilterParams
{
    public WateringFrequency[]? WateringFrequency { get; set; }
    public ToxicCategory[]? ToxicCategories { get; set; }
    public Illumination[]? Illumination { get; set; }
}