namespace FlowerApp.Domain.DbModels;

public class UserAnswer
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int QuestionId { get; set; }
    public int AnswersSize { get; set; }
    public int AnswerMask { get; set; }
}