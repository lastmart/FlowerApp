using AutoMapper;
using FlowerApp.Data;
using Microsoft.EntityFrameworkCore;
using AppUser = FlowerApp.Domain.Models.UserModels.User;
using DbUser = FlowerApp.Data.DbModels.Users.User;

namespace FlowerApp.Service.Storages;

public class UsersStorage : IUserStorage
{
    private readonly FlowerAppContext dbContext;
    private readonly IMapper mapper;

    public UsersStorage(FlowerAppContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<AppUser?> Get(string googleId)
    {
        return mapper.Map<AppUser>(await dbContext.Users.FirstOrDefaultAsync(u => u.GoogleId == googleId));
    }

    public async Task<AppUser?> GetByGoogleId(string googleId)
    {
        return mapper.Map<AppUser>(await dbContext.Users.FirstOrDefaultAsync(user => user.GoogleId == googleId));
    }

    public async Task<IList<AppUser>> Get(string[] ids)
    {
        return await dbContext.Users
            .Where(u => ids.Contains(u.GoogleId))
            .Select(u => mapper.Map<AppUser>(u))
            .ToListAsync();
    }

    public async Task<bool> Create(AppUser model)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var dbModel = mapper.Map<DbUser>(model);
            await dbContext.Users.AddAsync(dbModel);
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

    public async Task<bool> Update(AppUser model)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var dbUser = await dbContext.Users.FindAsync(model.GoogleId);

            if (dbUser == null)
                return false;

            CopyUser(dbUser, model);
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

    public async Task<bool> Delete(string googleId)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var user = await dbContext.Users.FindAsync(googleId);
            if (user == null)
                return true;
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

    private static void CopyUser(DbUser dbUser, AppUser appUser)
    {
        dbUser.Name = appUser.Name;
        dbUser.Email = appUser.Email;
        dbUser.Surname = appUser.Surname;
        dbUser.Telegram = appUser.Telegram;
    }
}