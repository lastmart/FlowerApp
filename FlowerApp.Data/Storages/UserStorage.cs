using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Data.Storages;

public class UserStorage : IUserStorage
{
    private readonly FlowerAppContext context;

    public UserStorage(FlowerAppContext context)
    {
        this.context = context;
    }

    public async Task<User?> Get(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> Create(User user)
    {
        try
        {
            await context.Users.AddAsync(user);
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> Update(Guid id,User User)
    {
        var existingUser = await context.Users.FindAsync(id);
        if (existingUser == null) return false;
        
        existingUser.Name = User.Name;
        existingUser.Surname = User.Surname;
        existingUser.Email = User.Email;
        existingUser.Telegram = User.Telegram;
        
        await context.SaveChangesAsync();
        return true;
    }
}