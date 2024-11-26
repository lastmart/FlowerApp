using FlowerApp.Domain.DbModels;

namespace FlowerApp.Data.Storages;

public interface IQuestionsStorage: IStorage<Question, int>
{
    Task<IList<Question>> GetAll();
}