using FlowerApp.Data;
using Flower = FlowerApp.Data.DbModels.Flowers.Flower;
using ToxicCategory = FlowerApp.Data.DbModels.Flowers.ToxicCategory;
using WateringFrequency = FlowerApp.Data.DbModels.Flowers.WateringFrequency;
using Illumination = FlowerApp.Data.DbModels.Flowers.Illumination;

namespace FlowerApp.Service.Database;

public class DataSeeder
{
    private readonly FlowerAppContext context;

    public DataSeeder(FlowerAppContext context)
    {
        this.context = context;
    }

    public async Task SeedDataAsync()
    {
        if (!context.Flowers.Any())
        {
            var flowers = new List<Flower>
            {
                new()
                {
                    Id = 1,
                    Name = "Rose",
                    ScientificName = "Rosa",
                    AppearanceDescription = "Beautiful red flower",
                    CareDescription = "Needs regular watering and sunlight",
                    PhotoUrl = "https://example.com/rose.jpg",
                    WateringFrequency = WateringFrequency.OnceAWeek,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.People | ToxicCategory.Kids
                },
                new()
                {
                    Id = 2,
                    Name = "Lily",
                    ScientificName = "Lilium",
                    AppearanceDescription = "Elegant white flower",
                    CareDescription = "Needs moderate watering and indirect sunlight",
                    PhotoUrl = "https://example.com/lily.jpg",
                    WateringFrequency = WateringFrequency.OnceAWeek,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.Pets
                },
                new()
                {
                    Id = 3,
                    Name = "Tulip",
                    ScientificName = "Tulipa",
                    AppearanceDescription = "Bright and colorful flower",
                    CareDescription = "Needs well-drained soil and moderate sunlight",
                    PhotoUrl = "https://example.com/tulip.jpg",
                    WateringFrequency = WateringFrequency.TwiceAWeek,
                    Illumination = Illumination.PartialShade,
                    ToxicCategory = ToxicCategory.People | ToxicCategory.Kids | ToxicCategory.Pets
                },
                new()
                {
                    Id = 4,
                    Name = "Sunflower",
                    ScientificName = "Helianthus",
                    AppearanceDescription = "Large yellow flower",
                    CareDescription = "Needs full sunlight and regular watering",
                    PhotoUrl = "https://example.com/sunflower.jpg",
                    WateringFrequency = WateringFrequency.OnceAMonth,
                    Illumination = Illumination.AverageIllumination,
                    ToxicCategory = ToxicCategory.None
                },
                new()
                {
                    Id = 5,
                    Name = "Orchid",
                    ScientificName = "Orchidaceae",
                    AppearanceDescription = "Exotic and colorful flower",
                    CareDescription = "Needs indirect sunlight and careful watering",
                    PhotoUrl = "https://example.com/orchid.jpg",
                    WateringFrequency = WateringFrequency.TwiceAWeek,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.People | ToxicCategory.Kids | ToxicCategory.Pets
                },
                new()
                {
                    Id = 6,
                    Name = "Daisy",
                    ScientificName = "Bellis perennis",
                    AppearanceDescription = "Simple and cheerful flower",
                    CareDescription = "Needs moderate sunlight and regular watering",
                    PhotoUrl = "https://example.com/daisy.jpg",
                    WateringFrequency = WateringFrequency.OnceAMonth,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.None
                },
                new()
                {
                    Id = 7,
                    Name = "Carnation",
                    ScientificName = "Dianthus caryophyllus",
                    AppearanceDescription = "Fragrant and colorful flower",
                    CareDescription = "Needs well-drained soil and moderate sunlight",
                    PhotoUrl = "https://example.com/carnation.jpg",
                    WateringFrequency = WateringFrequency.OnceEveryTwoWeeks,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.None
                },
                new()
                {
                    Id = 8,
                    Name = "Peony",
                    ScientificName = "Paeonia",
                    AppearanceDescription = "Large and fragrant flower",
                    CareDescription = "Needs full sunlight and regular watering",
                    PhotoUrl = "https://example.com/peony.jpg",
                    WateringFrequency = WateringFrequency.OnceAMonth,
                    Illumination = Illumination.PartialShade,
                    ToxicCategory = ToxicCategory.None
                },
                new()
                {
                    Id = 9,
                    Name = "Marigold",
                    ScientificName = "Tagetes",
                    AppearanceDescription = "Bright and cheerful flower",
                    CareDescription = "Needs full sunlight and regular watering",
                    PhotoUrl = "https://example.com/marigold.jpg",
                    WateringFrequency = WateringFrequency.OnceEveryTwoWeeks,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.None
                },
                new()
                {
                    Id = 10,
                    Name = "Lavender",
                    ScientificName = "Lavandula",
                    AppearanceDescription = "Fragrant and purple flower",
                    CareDescription = "Needs full sunlight and well-drained soil",
                    PhotoUrl = "https://example.com/lavender.jpg",
                    WateringFrequency = WateringFrequency.OnceAWeek,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.None
                }
            };

            context.Flowers.AddRange(flowers);

            await context.SaveChangesAsync();
        }
    }
}