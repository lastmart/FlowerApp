public enum LightLevel
{
    Low,
    Medium,
    High
}
public class Flower
{
    public string scientificName { get; set; }
    public string Name { get; set; }
    public string DescriptionFlower { get; set; }
    public bool IsToxic { get; set; }
    public int WateringFrequency { get; set; } // В днях
    public LightLevel LightRequirements { get; set; }
    public string TransplantingInfo { get; set; }

    public Flower(int id, string name, bool isToxic, int wateringFrequency, 
        LightLevel lightRequirements, string transplantingInfo)
    {
        Id = id;
        Name = name;
        IsToxic = isToxic;
        WateringFrequency = wateringFrequency;
        LightRequirements = lightRequirements;
        TransplantingInfo = transplantingInfo;
    }
}