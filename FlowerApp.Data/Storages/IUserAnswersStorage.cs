using FlowerApp.Domain.DbModels;

namespace FlowerApp.Data.Storages;

public interface IUserAnswersStorage: IStorage<UserAnswer, int>
{
    Task<IList<UserAnswer>> GetByUser(Guid userId);
    Task<IList<UserAnswer>> GetByQuestion(int questionId);
}