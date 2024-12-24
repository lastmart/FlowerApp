using AutoMapper;
using FlowerApp.Data;
using Microsoft.EntityFrameworkCore;
using DbQuestion = FlowerApp.Data.DbModels.Surveys.SurveyQuestion;
using AppQuestion = FlowerApp.Domain.Models.RecommendationModels.SurveyQuestion;

namespace FlowerApp.Service.Storages;

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
        return mapper.Map<AppQuestion>(await dbContext.Questions.FirstOrDefaultAsync(q => q.Id == id));
    }

    public async Task<IList<AppQuestion>> Get(int[] ids)
    {
        return await dbContext.Questions
            .Where(q => ids.Contains(q.Id))
            .Select(q => mapper.Map<AppQuestion>(q))
            .ToListAsync();
    }

    public async Task<bool> Create(AppQuestion model)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var dbModel = mapper.Map<DbQuestion>(model);
            await dbContext.Questions.AddAsync(dbModel);
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
            dbContext.Questions.Update(dbModel);
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
            var question = await dbContext.Questions.FindAsync(id);
            if (question == null) return true;
            dbContext.Questions.Remove(question);
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

    public async Task<IList<AppQuestion>> GetAll()
    {
        return await dbContext.Questions
            .Select(q => mapper.Map<AppQuestion>(q))
            .ToListAsync();
    }
}