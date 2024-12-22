namespace FlowerApp.DTOs.Common.Flowers;

public class Flower
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ScientificName { get; set; }
    public string AppearanceDescription { get; set; }
    public string CareDescription { get; set; }
    public Uri PhotoUrl { get; set; }
    public string WateringFrequency { get; set; }
    public float Size { get; set; }
    public List<string> ToxicCategory { get; set; }
    public string Illumination { get; set; }
}