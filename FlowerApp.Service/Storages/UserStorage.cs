using AutoMapper;
using FlowerApp.Data;
using FlowerApp.Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using DBUser = FlowerApp.Data.DbModels.Users.User;

namespace FlowerApp.Service.Storages;

public class UserStorage : IUserStorage
{
    private readonly FlowerAppContext context;
    private readonly IMapper mapper;

    public UserStorage(FlowerAppContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<User?> Get(Guid id)
    {
        return mapper.Map<User>(await context.Users.FirstOrDefaultAsync(u => u.Id == id));
    }

    public async Task<bool> Create(User user)
    {
        try
        {
            await context.Users.AddAsync(mapper.Map<DBUser>(user));
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> Update(Guid id, User User)
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