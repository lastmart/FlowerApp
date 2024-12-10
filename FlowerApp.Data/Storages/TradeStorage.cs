using FlowerApp.Domain.Common;
using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Data.Storages;

public class TradeStorage : ITradeStorage
{
    private readonly FlowerAppContext context;

    public TradeStorage(FlowerAppContext context)
    {
        this.context = context;
    }

    public async Task<Trade?> Get(Guid id)
    {
        var trade = await context.Trades.FirstOrDefaultAsync(t => t.Id == id);

        if (trade != null)
        {
            if (trade.ExpiresAt < DateTime.UtcNow && trade.IsActive)
            {
                trade.IsActive = false;
                await context.SaveChangesAsync();
            }

            if (!trade.IsActive)
            {
                return null;
            }
        }

        return trade;
    }


    public async Task<IEnumerable<Trade>> GetAll(Pagination pagination, string? location, string? userId,
        bool includeUserTrades)
    {
        var query = context.Trades.AsQueryable();

        foreach (var trade in query)
        {
            if (trade.ExpiresAt < DateTime.UtcNow && trade.IsActive)
            {
                trade.IsActive = false;
            }
        }

        query = query.Where(t => t.IsActive);

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(t => t.Location.Contains(location));
        }

        if (!string.IsNullOrEmpty(userId))
        {
            var userGuid = Guid.Parse(userId);
            query = query.Where(t => !((t.UserIdentifier == userGuid) ^ includeUserTrades));
        }

        var result = await query
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .ToListAsync();

        await context.SaveChangesAsync();

        return result;
    }

    public async Task<bool> Create(Trade trade)
    {
        try
        {
            await context.Trades.AddAsync(trade);
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> Update(Guid id, Trade trade)
    {
        var existingTrade = await context.Trades.FindAsync(id);
        if (existingTrade == null) return false;

        existingTrade.FlowerName = trade.FlowerName;
        existingTrade.PreferredTrade = trade.PreferredTrade;
        existingTrade.Location = trade.Location;
        existingTrade.Description = trade.Description;
        existingTrade.ExpiresAt = trade.ExpiresAt;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeactivateTrade(Guid id)
    {
        var trade = await context.Trades.FindAsync(id);
        if (trade == null) return false;

        trade.IsActive = false;
        return await context.SaveChangesAsync() > 0;
    }
}