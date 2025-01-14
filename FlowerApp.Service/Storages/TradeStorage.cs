using AutoMapper;
using FlowerApp.Data;
using FlowerApp.DTOs.Common;
using Microsoft.EntityFrameworkCore;
using AppTrade = FlowerApp.Domain.Models.TradeModels.Trade;
using DbTrade = FlowerApp.Data.DbModels.Trades.Trade;

namespace FlowerApp.Service.Storages;

public class TradeStorage : ITradeStorage
{
    private readonly FlowerAppContext dbContext;
    private readonly IMapper mapper;

    public TradeStorage(FlowerAppContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<AppTrade?> Get(int id)
    {
        var trade = await dbContext.Trades.FirstOrDefaultAsync(t => t.Id == id);
        return mapper.Map<AppTrade>(trade);
    }

    public async Task<IList<AppTrade>> Get(int[] ids)
    {
        return await dbContext.Trades
            .Where(trade => ids.Contains(trade.Id))
            .Select(trade => mapper.Map<AppTrade>(trade))
            .ToListAsync();
    }

    public async Task<IList<AppTrade>> GetOtherUsersTrades(
        Pagination pagination, 
        string? location, 
        string? excludeUserId)
    {
        var query = dbContext.Trades
            .Where(t => t.IsActive && t.UserId != excludeUserId);

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(t => t.Location.Contains(location));
        }

        var trades = await query
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .Select(trade => mapper.Map<AppTrade>(trade))
            .ToListAsync();
        
        
        foreach (var trade in trades)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.GoogleId == trade.UserId);
            trade.AuthorName = user?.Name ?? string.Empty;
            trade.ContactData = user?.Telegram ?? string.Empty;
        }

        return trades;
    }

    public async Task<IList<AppTrade>> GetUserTrades(
        Pagination pagination, 
        string? location, 
        string userId)
    {
        var query = dbContext.Trades
            .Where(t => t.IsActive && t.UserId == userId);

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(t => t.Location.Contains(location));
        }

        var trades = await query
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .Select(trade => mapper.Map<AppTrade>(trade))
            .ToListAsync();
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.GoogleId == userId);
        
        foreach (var trade in trades)
        {
            trade.AuthorName = user?.Name ?? string.Empty;
            trade.ContactData = user?.Telegram ?? string.Empty;
        }

        return trades;
    }

    public async Task<bool> Create(AppTrade trade)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await dbContext.Trades.AddAsync(mapper.Map<DbTrade>(trade));
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

    public async Task<bool> Update(AppTrade trade)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var dbTrade = await dbContext.Trades.FindAsync(trade.Id);
            if (dbTrade == null)
                return false;

            mapper.Map(trade, dbTrade);
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
            var trade = await dbContext.Trades.FindAsync(id);
            if (trade == null)
                return true;
            
            dbContext.Trades.Remove(trade);
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

    public async Task<bool> DeactivateTrade(int id)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var trade = await dbContext.Trades.FindAsync(id);
            if (trade == null)
                return false;

            trade.IsActive = false;
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