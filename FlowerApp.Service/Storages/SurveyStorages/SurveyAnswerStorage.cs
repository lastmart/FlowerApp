using AutoMapper;
using FlowerApp.Data;
using Microsoft.EntityFrameworkCore;
using AppSurveyAnswer = FlowerApp.Domain.Models.RecommendationModels.SurveyAnswer;
using DbSurveyAnswer = FlowerApp.Data.DbModels.Surveys.SurveyAnswer;

namespace FlowerApp.Service.Storages.SurveyStorages;

public class SurveyAnswerStorage : ISurveyAnswerStorage
{
    private readonly FlowerAppContext flowerAppContext;
    private readonly IMapper mapper;

    public SurveyAnswerStorage(FlowerAppContext flowerAppContext, IMapper mapper)
    {
        this.flowerAppContext = flowerAppContext;
        this.mapper = mapper;
    }

    public async Task<AppSurveyAnswer?> Get(int id)
    {
        var dbAnswer = await flowerAppContext.SurveyAnswers.FirstOrDefaultAsync(f => f.Id == id);
        return mapper.Map<AppSurveyAnswer>(dbAnswer);
    }

    public async Task<IList<AppSurveyAnswer>> Get(int[] ids)
    {
        return await flowerAppContext.SurveyAnswers
            .Where(f => ids.Contains(f.Id))
            .Select(dbSurvey => mapper.Map<AppSurveyAnswer>(dbSurvey))
            .ToListAsync();
    }

    public async Task<bool> Create(AppSurveyAnswer model)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();

        try
        {
            await flowerAppContext.SurveyAnswers.AddAsync(mapper.Map<DbSurveyAnswer>(model));
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

    public async Task<bool> Update(AppSurveyAnswer model)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();

        try
        {
            flowerAppContext.SurveyAnswers.Update(mapper.Map<DbSurveyAnswer>(model));
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

    public async Task<bool> Delete(int id)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();

        try
        {
            var surveyAnswer = await flowerAppContext.SurveyAnswers.FindAsync(id);

            if (surveyAnswer == null)
                return true;

            flowerAppContext.SurveyAnswers.Remove(surveyAnswer);
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
}