using FlowerApp.Domain.DbModels;

namespace FlowerApp.Domain.ApplicationModels.FlowerModels;

public class Flower
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ScientificName { get; set; }
    public string AppearanceDescription { get; set; }
    public string CareDescription { get; set; }
    public Uri PhotoUrl { get; set; }
    public WateringFrequency WateringFrequency { get; set; }
    public float Size { get; set; }
    public int LightParametersId { get; set; }
    public List<string> ToxicCategory { get; set; }
    public Illumination Illumination { get; set; }
}