using System.ComponentModel.DataAnnotations;
public class FlowerSortOptions
{
    [Required(ErrorMessage = "SortBy is required.")]
    [RegularExpression("^(wateringfrequency|name|scientificname|lightrequirements|transplantfrequency|istoxic)$",
        ErrorMessage = "SortBy must be one of: wateringfrequency, name, scientificname, lightrequirements, " +
                       "transplantfrequency, isToxic.")]
    public string SortBy { get; set; }
    public bool IsDescending { get; set; }
}