namespace FlowerApp.Data.DbModels.Flowers;

[Flags]
public enum Soil
{
    Any = 0b0000_0000,
    UniversalSoil = 0b0000_0001,
    CactiAndSucculentsSoil = 0b0000_0010,
    OrchidsSoil = 0b0000_0100,
    CitrusFruitsSoil = 0b0000_1000,
    PalmTreesAndOrnamentalDeciduousPlantsSoil = 0b0001_0000,
    VioletsSoil = 0b0010_0000,
    SeedlingsSoil = 0b0100_0000,
    CoconutSubstrate = 0b1000_0000
}