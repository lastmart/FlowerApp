using FlowerApp.Data;
using FlowerApp.Data.DbModels.Flowers;
using FlowerApp.Data.DbModels.Surveys;
using FlowerApp.Data.DbModels.Users;

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
        // if (!context.Users.Any())
        // {
        //     var users = new List<User>
        //     {
        //         new() { Name = "Mark", Email = "test@123.ru", Surname = "Surname", Telegram = "dasvjdv" },
        //         new() { Name = "Alice", Email = "test@123.ru", Surname = "Surname", Telegram = "dasvjdv" }
        //     };
        //
        //     context.Users.AddRange(users);
        //     await context.SaveChangesAsync();
        // }

        if (!context.Questions.Any())
        {
            // var questions = new List<SurveyQuestion>
            // {
            //     new()
            //     {
            //         Id = 1,
            //         Text = "Любите ли вы растения?",
            //         Variants = string.Join(";", new List<string> { "Да", "Нет" }),
            //         QuestionType = QuestionType.SingleAnswer
            //     },
            // };
            //     new Survey
            //     {
            //         Id = 2,
            //         Question = "Сколько раз в неделю вы готовы поливать цветы?",
            //         Answer = 2,
            //         AnswerOptions = new List<string>() { "Меньше 3 раз", "Больше 3 раз" }
            //     },
            //     new Survey
            //     {
            //         Id = 3,
            //         Question = "Есть ли у вас домашние животные или дети?",
            //         Answer = 2,
            //         AnswerOptions = new List<string>() { "Да", "Нет" }
            //     },
            //     new Survey
            //     {
            //         Id = 4,
            //         Question = "Готовы ли вы следить за освещением для цветка?",
            //         Answer = 3,
            //         AnswerOptions = new List<string>() { "Да", "Нет", "Специальное освещение" }
            //     },
            //     new Survey
            //     {
            //         Id = 5,
            //         Question = "Готовы ли вы часто пересаживать цветок?",
            //         Answer = 2,
            //         AnswerOptions = new List<string>() { "Да", "Нет" }
            //     }
            // };
            //
            // context.Questions.AddRange(questions);
            // await context.SaveChangesAsync();
        }

        // if (!context.SurveyAnswers.Any() && context.Users.Any())
        // {
        //     var firstUser = context.Users.First();
        //     var userAnswers = new List<UserAnswer>
        //     {
        //         new UserAnswer
        //         {
        //             Id = 1,
        //             UserId = firstUser.Id,
        //             QuestionId = 1,
        //             AnswersSize = 1,
        //             AnswerMask = 1
        //         },
        //         new UserAnswer
        //         {
        //             Id = 2,
        //             UserId = firstUser.Id,
        //             QuestionId = 2,
        //             AnswersSize = 1,
        //             AnswerMask = 0
        //         },
        //         new UserAnswer
        //         {
        //             Id = 3,
        //             UserId = firstUser.Id,
        //             QuestionId = 2,
        //             AnswersSize = 1,
        //             AnswerMask = 1
        //         },
        //         new UserAnswer
        //         {
        //             Id = 4,
        //             UserId = firstUser.Id,
        //             QuestionId = 2,
        //             AnswersSize = 1,
        //             AnswerMask = 2
        //         },
        //         new UserAnswer
        //         {
        //             Id = 5,
        //             UserId = firstUser.Id,
        //             QuestionId = 2,
        //             AnswersSize = 1,
        //             AnswerMask = 0
        //         }
        //     };
        //
        //     context.SurveyAnswers.AddRange(userAnswers);
        //     await context.SaveChangesAsync();
        // }

        if (!context.Flowers.Any())
        {
            var flowers = new List<Flower>
            {
                new()
                {
                    Id = 1,
                    Name = "rose",
                    ScientificName = "rosa",
                    AppearanceDescription = "Beautiful red flower",
                    CareDescription = "Needs regular watering and sunlight",
                    PhotoUrl = "https://yt3.googleusercontent.com/RcRHIvIJHiYww-fIjs62ntgv1v_-wjQAVZ0fqLHCWpC2XqMtx9GH1SKeVfbuf39lyL02iREpDw=s900-c-k-c0x00ffffff-no-rj",
                    Size = 123,
                    Soil = Soil.OrchidsSoil,
                    WateringFrequency = WateringFrequency.OnceAWeek,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.People | ToxicCategory.Kids
                },
                new()
                {
                    Id = 2,
                    Name = "lily",
                    ScientificName = "lilium",
                    AppearanceDescription = "Elegant white flower",
                    CareDescription = "Needs moderate watering and indirect sunlight",
                    PhotoUrl = "https://yt3.googleusercontent.com/RcRHIvIJHiYww-fIjs62ntgv1v_-wjQAVZ0fqLHCWpC2XqMtx9GH1SKeVfbuf39lyL02iREpDw=s900-c-k-c0x00ffffff-no-rj",
                    Size = 123,
                    Soil = Soil.OrchidsSoil,
                    WateringFrequency = WateringFrequency.OnceAWeek,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.Pets
                },
                new()
                {
                    Id = 3,
                    Name = "tulip",
                    ScientificName = "tulipa",
                    AppearanceDescription = "Bright and colorful flower",
                    CareDescription = "Needs well-drained soil and moderate sunlight",
                    PhotoUrl = "https://yt3.googleusercontent.com/RcRHIvIJHiYww-fIjs62ntgv1v_-wjQAVZ0fqLHCWpC2XqMtx9GH1SKeVfbuf39lyL02iREpDw=s900-c-k-c0x00ffffff-no-rj",
                    Size = 123,
                    Soil = Soil.OrchidsSoil,
                    WateringFrequency = WateringFrequency.TwiceAWeek,
                    Illumination = Illumination.PartialShade,
                    ToxicCategory = ToxicCategory.People | ToxicCategory.Kids | ToxicCategory.Pets
                },
                new()
                {
                    Id = 4,
                    Name = "sunflower",
                    ScientificName = "helianthus",
                    AppearanceDescription = "Large yellow flower",
                    CareDescription = "Needs full sunlight and regular watering",
                    PhotoUrl = "https://yt3.googleusercontent.com/RcRHIvIJHiYww-fIjs62ntgv1v_-wjQAVZ0fqLHCWpC2XqMtx9GH1SKeVfbuf39lyL02iREpDw=s900-c-k-c0x00ffffff-no-rj",
                    Size = 123,
                    Soil = Soil.OrchidsSoil,
                    WateringFrequency = WateringFrequency.OnceAMonth,
                    Illumination = Illumination.AverageIllumination,
                    ToxicCategory = ToxicCategory.None
                },
                new()
                {
                    Id = 5,
                    Name = "orchid",
                    ScientificName = "orchidaceae",
                    AppearanceDescription = "Exotic and colorful flower",
                    CareDescription = "Needs indirect sunlight and careful watering",
                    PhotoUrl = "https://yt3.googleusercontent.com/RcRHIvIJHiYww-fIjs62ntgv1v_-wjQAVZ0fqLHCWpC2XqMtx9GH1SKeVfbuf39lyL02iREpDw=s900-c-k-c0x00ffffff-no-rj",
                    Size = 123,
                    Soil = Soil.OrchidsSoil,
                    WateringFrequency = WateringFrequency.TwiceAWeek,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.People | ToxicCategory.Kids | ToxicCategory.Pets
                },
                new()
                {
                    Id = 6,
                    Name = "daisy",
                    ScientificName = "bellis perennis",
                    AppearanceDescription = "Simple and cheerful flower",
                    CareDescription = "Needs moderate sunlight and regular watering",
                    PhotoUrl = "https://yt3.googleusercontent.com/RcRHIvIJHiYww-fIjs62ntgv1v_-wjQAVZ0fqLHCWpC2XqMtx9GH1SKeVfbuf39lyL02iREpDw=s900-c-k-c0x00ffffff-no-rj",
                    Size = 123,
                    Soil = Soil.OrchidsSoil,
                    WateringFrequency = WateringFrequency.OnceAMonth,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.None
                },
                new()
                {
                    Id = 7,
                    Name = "carnation",
                    ScientificName = "dianthus caryophyllus",
                    AppearanceDescription = "Fragrant and colorful flower",
                    CareDescription = "Needs well-drained soil and moderate sunlight",
                    PhotoUrl = "https://yt3.googleusercontent.com/RcRHIvIJHiYww-fIjs62ntgv1v_-wjQAVZ0fqLHCWpC2XqMtx9GH1SKeVfbuf39lyL02iREpDw=s900-c-k-c0x00ffffff-no-rj",
                    Size = 123,
                    WateringFrequency = WateringFrequency.OnceEveryTwoWeeks,
                    Soil = Soil.OrchidsSoil,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.None
                },
                new()
                {
                    Id = 8,
                    Name = "peony",
                    ScientificName = "paeonia",
                    AppearanceDescription = "Large and fragrant flower",
                    CareDescription = "Needs full sunlight and regular watering",
                    PhotoUrl = "https://yt3.googleusercontent.com/RcRHIvIJHiYww-fIjs62ntgv1v_-wjQAVZ0fqLHCWpC2XqMtx9GH1SKeVfbuf39lyL02iREpDw=s900-c-k-c0x00ffffff-no-rj",
                    Size = 123,
                    Soil = Soil.OrchidsSoil,
                    WateringFrequency = WateringFrequency.OnceAMonth,
                    Illumination = Illumination.PartialShade,
                    ToxicCategory = ToxicCategory.None
                },
                new()
                {
                    Id = 9,
                    Name = "marigold",
                    ScientificName = "tagetes",
                    AppearanceDescription = "Bright and cheerful flower",
                    CareDescription = "Needs full sunlight and regular watering",
                    PhotoUrl = "https://yt3.googleusercontent.com/RcRHIvIJHiYww-fIjs62ntgv1v_-wjQAVZ0fqLHCWpC2XqMtx9GH1SKeVfbuf39lyL02iREpDw=s900-c-k-c0x00ffffff-no-rj",
                    Size = 123,
                    WateringFrequency = WateringFrequency.OnceEveryTwoWeeks,
                    Soil = Soil.OrchidsSoil,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.None
                },
                new()
                {
                    Id = 10,
                    Name = "lavender",
                    ScientificName = "lavandula",
                    AppearanceDescription = "Fragrant and purple flower",
                    CareDescription = "Needs full sunlight and well-drained soil",
                    PhotoUrl = "https://yt3.googleusercontent.com/RcRHIvIJHiYww-fIjs62ntgv1v_-wjQAVZ0fqLHCWpC2XqMtx9GH1SKeVfbuf39lyL02iREpDw=s900-c-k-c0x00ffffff-no-rj",
                    Size = 123,
                    WateringFrequency = WateringFrequency.OnceAWeek,
                    Soil = Soil.OrchidsSoil,
                    Illumination = Illumination.Bright,
                    ToxicCategory = ToxicCategory.None
                }
            };

            context.Flowers.AddRange(flowers);

            await context.SaveChangesAsync();
        }
    }
}