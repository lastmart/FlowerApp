using System.ComponentModel.DataAnnotations;

namespace FlowerApp.Domain.DbModels;

public class SortOption
{
    [Required(ErrorMessage = "SortBy is required.")]
    [RegularExpression("^(wateringFrequency|name|scientificName|illuminationInSuites|durationInHours|isShadeTolerant|transplantFrequency|isToxic)$",
        ErrorMessage = "SortBy must be one of: wateringFrequency, name, scientificname, illuminationInSuites, " +
                       "durationInHours, isShadeTolerant, transplantFrequency, isToxic.")]
    public string SortBy { get; set; }
    public bool IsDescending { get; set; }
}

public class FlowerSortOptions
{
    public List<SortOption> SortOptions { get; set; }
}