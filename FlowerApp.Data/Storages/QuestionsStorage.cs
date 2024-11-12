using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Data.Storages;

public class QuestionsStorage: IQuestionsStorage
{
    private readonly FlowerAppContext dbContext;

    public QuestionsStorage(FlowerAppContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task<Question?> Get(int id)
    {
        return await dbContext.Questions.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IList<Question>> Get(int[] ids)
    {
        return await dbContext.Questions
            .Where(q => ids.Contains(q.Id))
            .ToListAsync();
    }

    public async Task<bool> Create(Question model)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await dbContext.Questions.AddAsync(model);
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
    
    public async Task<bool> Update(Question model)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            dbContext.Questions.Update(model);
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
    
    public async Task<IList<Question>> GetAll()
    {
        return await dbContext.Questions.ToListAsync();
    }
}