namespace FlowerApp.Data.DbModels.Flowers;

[Flags]
public enum Illumination
{
    Any = 0b000,
    Bright = 0b001,
    PartialShade = 0b010,
    AverageIllumination = 0b100
}