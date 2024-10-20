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

    public Flower(string scientificName, string name, string descriptionFlower, 
        int wateringFrequency, int transplantFrequency, 
        string descriptionCare, string photoUrl)
    {
        ScientificName = scientificName;
        Name = name;
        DescriptionFlower = descriptionFlower;
        WateringFrequency = wateringFrequency;
        TransplantFrequency = transplantFrequency;
        DescriptionCare = descriptionCare;
        PhotoUrl = photoUrl;
    }
}