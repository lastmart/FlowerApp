using FlowerApp.Data.DbModels.Flowers;

namespace FlowerApp.Domain.Models.FlowerModels;

public class Flower : Entity<int>
{
    public string Name { get; set; }
    public string ScientificName { get; set; }
    public string AppearanceDescription { get; set; }
    public string CareDescription { get; set; }
    public string PhotoBase64 { get; set; }
    public WateringFrequency WateringFrequency { get; set; }
    public Soil Soil { get; set; }
    public float Size { get; set; }
    public List<ToxicCategory> ToxicCategory { get; set; }
    public Illumination Illumination { get; set; }
}