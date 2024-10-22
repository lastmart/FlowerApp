using System.ComponentModel.DataAnnotations;
using FlowersCareAPI.Models;

public class Flower
{
    [Required]
    public string ScientificName { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int FId { get; set; }
    public string DescriptionFlower { get; set; }
    public int WateringFrequency { get; set; } // В днях
    public int TransplantFrequency { get; set; } // В месяцах
    public string DescriptionCare { get; set; }
    public string PhotoUrl { get; set; }
    
    public LightParameters LightParameters { get; set; }

    public ToxicCategory[]? ToxicCategories { get; set; }
    
    public bool IsToxic => ToxicCategories is { Length: > 0 };

    public Flower(int fid, string scientificName, string name, string descriptionFlower, 
        int wateringFrequency, int transplantFrequency, 
        string descriptionCare, string photoUrl, 
        LightParameters lightParameters, ToxicCategory[]? toxicCategories = null)
    {
        FId = fid;
        ScientificName = scientificName;
        Name = name;
        DescriptionFlower = descriptionFlower;
        WateringFrequency = wateringFrequency;
        TransplantFrequency = transplantFrequency;
        DescriptionCare = descriptionCare;
        PhotoUrl = photoUrl;
        LightParameters = lightParameters;
        ToxicCategories = toxicCategories;
    }
}