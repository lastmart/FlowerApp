using FlowerApp.Data.DbModels.Users;

namespace FlowerApp.Data.DbModels.Surveys;

public class Survey : Entity<int>
{
    public int UserId { get; set; }

    // public User User;
    public IEnumerable<SurveyAnswer> Answers { get; set; }
}