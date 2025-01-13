using AutoMapper;
using FlowerApp.Data;
using FlowerApp.Domain.Models.AuthModels;
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

    public async Task<AppUser?> Get(string id)
    {
        return mapper.Map<AppUser>(await dbContext.Users.FirstOrDefaultAsync(u => u.GoogleId == id));
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
            dbContext.Users.Update(dbUser);
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
    

    public async Task<bool> Delete(string id)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var user = await dbContext.Users.FindAsync(id);
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

    public static void CopyUser(DbUser dbUser, AppUser appUser)
    {
        if (appUser.Name != null) dbUser.Name = appUser.Name;
        if (appUser.Email != null) dbUser.Email = appUser.Email;
        if (appUser.Surname != null) dbUser.Surname = appUser.Surname;
        if (appUser.Telegram != null) dbUser.Telegram = appUser.Telegram;
    }
}