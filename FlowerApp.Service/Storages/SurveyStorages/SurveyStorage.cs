using AutoMapper;
using FlowerApp.Data;
using Microsoft.EntityFrameworkCore;
using DbSurvey = FlowerApp.Data.DbModels.Surveys.Survey;
using AppSurvey = FlowerApp.Domain.Models.RecommendationModels.Survey;
using DbSurveyAnswer = FlowerApp.Data.DbModels.Surveys.SurveyAnswer;

namespace FlowerApp.Service.Storages.SurveyStorages;

public class SurveyStorage : ISurveyStorage
{
    private readonly FlowerAppContext flowerAppContext;
    private readonly IMapper mapper;

    public SurveyStorage(FlowerAppContext dbContext, IMapper mapper)
    {
        flowerAppContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<AppSurvey?> Get(int id)
    {
        var dbSurvey = await flowerAppContext.Surveys
            .Include(s => s.Answers)
            .Include(s => s.User)
            .FirstOrDefaultAsync(survey => survey.Id == id);

        return mapper.Map<AppSurvey>(dbSurvey);
    }

    public async Task<AppSurvey> GetByUser(string userId)
    {
        var dbSurvey = await flowerAppContext.Surveys
            .Include(s => s.Answers)
            .Include(s => s.User)
            .FirstOrDefaultAsync(survey => survey.UserId == userId);
        
        return mapper.Map<AppSurvey>(dbSurvey);
    }

    public async Task<IList<AppSurvey>> Get(int[] ids)
    {
        return await flowerAppContext.Surveys
            .Include(s => s.Answers)
            .Include(s => s.User)
            .Where(survey => ids.Contains(survey.Id))
            .Select(dbSurvey => mapper.Map<AppSurvey>(dbSurvey))
            .ToListAsync();
    }

    public async Task<bool> Create(AppSurvey survey)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();

        try
        {
            var user = await flowerAppContext.Users.FirstOrDefaultAsync(user => user.GoogleId == survey.UserId);

            if (user is null)
                return false;

            var dbSurveyEntry = await flowerAppContext.Surveys.AddAsync(mapper.Map<DbSurvey>(survey));
            var dbSurvey = dbSurveyEntry.Entity;
            foreach (var dbSurveyAnswer in survey.Answers.Select(surveyAnswer => mapper.Map<DbSurveyAnswer>(surveyAnswer)))
            {
                dbSurveyAnswer.SurveyId = dbSurvey.Id; 
            }

            var result = await flowerAppContext.SaveChangesAsync() > 0;
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public Task<bool> Update(AppSurvey model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}