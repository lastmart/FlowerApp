using FlowerApp.Data.DbModels.Surveys;
using FlowerApp.Data.DbModels.Trades;

namespace FlowerApp.Data.DbModels.Users;

public class User : Entity<int>
{
    public string GoogleUserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Email { get; set; }
    public string? Telegram { get; set; }
    public int SurveyId { get; set; }

    public Survey Survey { get; set; }
    public IEnumerable<Trade> Trades { get; set; }
}