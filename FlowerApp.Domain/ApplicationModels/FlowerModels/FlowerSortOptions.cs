using System.ComponentModel.DataAnnotations;

namespace FlowerApp.Domain.ApplicationModels.FlowerModels;

public enum SortByOption
{
    WateringFrequency,
    Name,
    ScientificName,
    IlluminationInSuites,
    IsToxic
}

public class SortOption
{
    [Required(ErrorMessage = "SortBy is required.")]
    public SortByOption SortBy { get; set; }
    public bool IsDescending { get; set; }
}

public class FlowerSortOptions
{
    public List<SortOption> SortOptions { get; set; } = new ();
}