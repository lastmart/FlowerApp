namespace FlowerApp.Data.DbModels.Flowers;

[Flags]
public enum ToxicCategory
{
    None = 0,
    Kids = 1,
    Pets = 2,
    People = 4
}