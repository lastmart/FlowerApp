using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Data.Storages;

public class UserAnswersStorage: IUserAnswersStorage
{
    private readonly FlowerAppContext dbContext;
    
    public UserAnswersStorage(FlowerAppContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task<UserAnswer?> Get(int id)
    {
        return await dbContext.UserAnswers.FirstOrDefaultAsync(ua => ua.Id == id);
    }
    
    public async Task<IList<UserAnswer>> Get(int[] ids)
    {
        return await dbContext.UserAnswers
            .Where(ua => ids.Contains(ua.Id))
            .ToListAsync();
    }
    
    public async Task<bool> Create(UserAnswer model)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await dbContext.UserAnswers.AddAsync(model);
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
    
    public async Task<bool> Update(UserAnswer model)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            dbContext.UserAnswers.Update(model);
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
            var userAnswer = await dbContext.UserAnswers.FindAsync(id);
            if (userAnswer == null) return true;
            dbContext.UserAnswers.Remove(userAnswer);
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
    
    public async Task<IList<UserAnswer>> GetByUser(Guid userId)
    {
        return await dbContext.UserAnswers
            .Where(ua => ua.UserId == userId)
            .ToListAsync();
    }
    
    public async Task<IList<UserAnswer>> GetByQuestion(int questionId)
    {
        return await dbContext.UserAnswers
            .Where(ua => ua.QuestionId == questionId)
            .ToListAsync();
    }
}