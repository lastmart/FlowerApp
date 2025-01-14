using AutoMapper;
using FlowerApp.Data;
using Microsoft.EntityFrameworkCore;
using DbQuestion = FlowerApp.Data.DbModels.Surveys.SurveyQuestion;
using AppQuestion = FlowerApp.Domain.Models.RecommendationModels.SurveyQuestion;

namespace FlowerApp.Service.Storages.SurveyStorages;

public class SurveyQuestionsStorage : ISurveyQuestionsStorage
{
    private readonly FlowerAppContext dbContext;
    private readonly IMapper mapper;

    public SurveyQuestionsStorage(FlowerAppContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<AppQuestion?> Get(int id)
    {
        return mapper.Map<AppQuestion>(await dbContext.SurveyQuestions.FirstOrDefaultAsync(q => q.Id == id));
    }

    public async Task<IList<AppQuestion>> Get(int[] ids)
    {
        return await dbContext.SurveyQuestions
            .Where(q => ids.Contains(q.Id))
            .Select(q => mapper.Map<AppQuestion>(q))
            .ToListAsync();
    }

    public async Task<IList<AppQuestion>> GetAll()
    {
        return await dbContext.SurveyQuestions
            .Select(q => mapper.Map<AppQuestion>(q))
            .ToListAsync();
    }

    public async Task<bool> Create(AppQuestion model)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var dbModel = mapper.Map<DbQuestion>(model);
            await dbContext.SurveyQuestions.AddAsync(dbModel);
            var result = await dbContext.SaveChangesAsync() > 0;
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<bool> Update(AppQuestion model)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var dbModel = mapper.Map<DbQuestion>(model);
            dbContext.SurveyQuestions.Update(dbModel);
            var result = await dbContext.SaveChangesAsync() > 0;
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
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var question = await dbContext.SurveyQuestions.FindAsync(id);
            if (question == null) return true;
            dbContext.SurveyQuestions.Remove(question);
            var result = await dbContext.SaveChangesAsync() > 0;
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