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
        if (!context.Users.Any())
        {
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Name = "Mark" },
                new User { Id = Guid.NewGuid(), Name = "Alice" }
            };
        
            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }
        
        if (!context.Questions.Any())
        {
            var questions = new List<Question>
            {
                new Question
                {
                    Id = 1,
                    QuestionText = "Любите ли вы растения?",
                    AnswerSize = 2,
                    AnswerOptions = new List<string>() { "Да", "Нет" }
                },
                new Question
                {
                    Id = 2,
                    QuestionText = "Сколько раз в неделю вы готовы поливать цветы?",
                    AnswerSize = 2,
                    AnswerOptions = new List<string>() { "Меньше 3 раз", "Больше 3 раз" }
                },
                new Question
                {
                    Id = 3,
                    QuestionText = "Есть ли у вас домашние животные или дети?",
                    AnswerSize = 2,
                    AnswerOptions = new List<string>() { "Да", "Нет" }
                },
                new Question
                {
                    Id = 4,
                    QuestionText = "Готовы ли вы следить за освещением для цветка?",
                    AnswerSize = 3,
                    AnswerOptions = new List<string>() { "Да", "Нет", "Специальное освещение" }
                },
                new Question
                {
                    Id = 5,
                    QuestionText = "Готовы ли вы часто пересаживать цветок?",
                    AnswerSize = 2,
                    AnswerOptions = new List<string>() { "Да", "Нет" }
                }
            };

            context.Questions.AddRange(questions);
            await context.SaveChangesAsync();
        }
        
        if (!context.UserAnswers.Any() && context.Users.Any())
        {
            var firstUser = context.Users.First();
            var userAnswers = new List<UserAnswer>
            {
                new UserAnswer
                {
                    Id = 1,
                    UserId = firstUser.Id,
                    QuestionId = 1,
                    AnswersSize = 1,
                    AnswerMask = 1 
                },
                new UserAnswer
                {
                    Id = 2,
                    UserId = firstUser.Id,
                    QuestionId = 2,
                    AnswersSize = 1,
                    AnswerMask = 0 
                },
                new UserAnswer
                {
                    Id = 3,
                    UserId = firstUser.Id,
                    QuestionId = 2,
                    AnswersSize = 1,
                    AnswerMask = 1 
                },
                new UserAnswer
                {
                    Id = 4,
                    UserId = firstUser.Id,
                    QuestionId = 2,
                    AnswersSize = 1,
                    AnswerMask = 2 
                },
                new UserAnswer
                {
                    Id = 5,
                    UserId = firstUser.Id,
                    QuestionId = 2,
                    AnswersSize = 1,
                    AnswerMask = 0
                }
            };

            context.UserAnswers.AddRange(userAnswers);
            await context.SaveChangesAsync();
        }
        
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