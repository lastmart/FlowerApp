namespace FlowerApp.Domain.DTOModels;

public class FlowerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ScientificName { get; set; }
    public string AppearanceDescription { get; set; }
    public string CareDescription { get; set; }
    public string PhotoUrl { get; set; }
    public DateTime WateringFrequency { get; set; }
    public DateTime TransplantFrequency { get; set; }
    public int LightParametersId { get; set; }
    public List<string> ToxicCategory { get; set; }
    public LightParametersDto LightParameters { get; set; }
}