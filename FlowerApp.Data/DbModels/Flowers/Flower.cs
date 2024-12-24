namespace FlowerApp.Data.DbModels.Flowers;

public class Flower : Entity<int>
{
    public string Name { get; set; }
    public string ScientificName { get; set; }
    public string AppearanceDescription { get; set; }
    public string CareDescription { get; set; }
    public string PhotoUrl { get; set; }
    public float Size { get; set; }
    public WateringFrequency WateringFrequency { get; set; }
    public Soil Soil { get; set; }
    public ToxicCategory ToxicCategory { get; set; }
    public Illumination Illumination { get; set; }
}