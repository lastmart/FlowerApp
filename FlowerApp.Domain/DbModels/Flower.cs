namespace FlowerApp.Domain.DbModels;

public class Flower : Entity<int>
{
    public string Name { get; set; }
    public string ScientificName { get; set; }
    public string AppearanceDescription { get; set; }
    public string CareDescription { get; set; }
    public string PhotoUrl { get; set; }
    public DateTime WateringFrequency { get; set; }
    public DateTime TransplantFrequency { get; set; }
    public int LightParametersId { get; set; }
    public ToxicCategory ToxicCategory { get; set; }

    public LightParameters LightParameters { get; set; }
}