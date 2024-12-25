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


    public async Task<IEnumerable<AppTrade>> Get(Pagination pagination, string? location, int? excludeId)
    {
        var query = dbContext.Trades.Where(t => t.IsActive);

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(t => t.Location.Contains(location));
        }

        if (excludeId != null)
        {
            query = query.Where(trade => trade.UserId != excludeId);
        }

        var result = await query
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .Select(trade => mapper.Map<AppTrade>(trade))
            .ToListAsync();

        await dbContext.SaveChangesAsync();

        return result;
    }

    public async Task<bool> Create(AppTrade trade)
    {
        try
        {
            await dbContext.Trades.AddAsync(mapper.Map<DbTrade>(trade));
            return await dbContext.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> Update(AppTrade trade)
    {
        var dbTrade = await dbContext.Trades.FindAsync(trade.Id);
        if (dbTrade == null)
            return false;

        CopyTrade(dbTrade, trade);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var user = await dbContext.Trades.FindAsync(id);
            if (user == null)
                return true;
            dbContext.Trades.Remove(user);
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
        var trade = await dbContext.Trades.FindAsync(id);
        if (trade == null)
            return false;

        trade.IsActive = false;
        return await dbContext.SaveChangesAsync() > 0;
    }

    private static void CopyTrade(DbTrade dbTrade, AppTrade appTrade)
    {
        dbTrade.FlowerName = appTrade.FlowerName;
        dbTrade.PreferredTrade = appTrade.PreferredTrade;
        dbTrade.Location = appTrade.Location;
        dbTrade.Description = appTrade.Description;
        dbTrade.ExpiresAt = appTrade.ExpiresAt;
    }
}