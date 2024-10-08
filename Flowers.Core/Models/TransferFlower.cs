namespace FlowersCareAPI.Models;

public class TransferFlower
{
    public string ScientificName { get; set; }
    public string Name { get; set; }
    public bool IsToxic { get; set; }
    public int WateringFrequency { get; set; } // В днях
    public LightLevel LightRequirements { get; set; }
    public int TransplantFrequency { get; set; } // В месяцах
}