using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Data.Storages;

public class UsersStorage: IUserStorage
{
    private readonly FlowerAppContext dbContext;

    public UsersStorage(FlowerAppContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task<User?> Get(Guid id)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<IList<User>> Get(Guid[] ids)
    {
        return await dbContext.Users
            .Where(u => ids.Contains(u.Id))
            .ToListAsync();
    }
    
    public async Task<bool> Create(User model)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await dbContext.Users.AddAsync(model);
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
    
    public async Task<bool> Update(User model)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            dbContext.Users.Update(model);
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

    public async Task<bool> Delete(Guid id)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user == null) return true;
            dbContext.Users.Remove(user);
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
    
    public async Task<User> CreateUser(string name)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        var result = await Create(user);
        if (!result)
        {
            throw new Exception("Failed to create user");
        }

        return user;
    }
}