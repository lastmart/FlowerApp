using System.ComponentModel.DataAnnotations;

public enum LightLevel
{
    Low,
    Medium,
    High
}

public class Flower
{
    [Required]
    public string ScientificName { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int FId { get; set; }
    public string DescriptionFlower { get; set; }
    public bool IsToxic { get; set; }
    public int WateringFrequency { get; set; } // В днях
    public LightLevel LightRequirements { get; set; }
    public int TransplantFrequency { get; set; } // В месяцах
    public string DescriptionCare { get; set; }
    public string PhotoUrl { get; set; }

    public Flower(string scientificName, string name, string descriptionFlower, bool isToxic, 
        int wateringFrequency, LightLevel lightRequirements, int transplantFrequency, 
        string descriptionCare, string photoUrl)
    {
        ScientificName = scientificName;
        Name = name;
        DescriptionFlower = descriptionFlower;
        IsToxic = isToxic;
        WateringFrequency = wateringFrequency;
        LightRequirements = lightRequirements;
        TransplantFrequency = transplantFrequency;
        DescriptionCare = descriptionCare;
        PhotoUrl = photoUrl;
    }
}