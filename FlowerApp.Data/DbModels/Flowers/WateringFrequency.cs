namespace FlowerApp.Data.DbModels.Flowers;

[Flags]
public enum WateringFrequency
{
    Any = 0b0000,
    OnceAWeek = 0b0001,
    TwiceAWeek = 0b0010,
    OnceEveryTwoWeeks = 0b0100,
    OnceAMonth = 0b1000
}