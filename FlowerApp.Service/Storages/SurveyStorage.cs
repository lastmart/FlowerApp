using FlowerApp.Data;
using FlowerApp.Domain.Models.RecommendationModels;

namespace FlowerApp.Service.Storages;

public class SurveyStorage: ISurveyStorage
{
    private readonly FlowerAppContext dbContext;

    public SurveyStorage(FlowerAppContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task<bool> Create(Survey survey)
    {
        throw new NotImplementedException();
    }
}