namespace FlowerApp.Data.DbModels.Flowers;

[Flags]
public enum ToxicCategory
{
    Any = 0b0000,
    None = 0b0001,
    Kids = 0b0010,
    Pets = 0b0100,
    People = 0b1000
}