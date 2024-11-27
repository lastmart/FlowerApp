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
        return await context.Trades.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Trade>> GetAll(Pagination pagination, string? location, string? userId, bool excludeUserTrades)
    {
        var query = context.Trades.AsQueryable();
        
        query = query.Where(t => t.IsActive);

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(t => t.Location.Contains(location));
        }

        if (excludeUserTrades && !string.IsNullOrEmpty(userId))
        {
            var userGuid = Guid.Parse(userId);
            query = query.Where(t => t.UserIdentifier != userGuid);
        }

        return await query
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .ToListAsync();
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
}