using FlowerApp.Domain.DbModels;

namespace FlowerApp.Data.Storages;

public class LightParametersStorage : IStorageBase<LightParameters, int>
{
    private readonly FlowerAppContext flowerAppContext;
    
    public LightParametersStorage(FlowerAppContext flowerAppContext)
    {
        this.flowerAppContext = flowerAppContext;
    }
    public LightParameters? Get(int id) => flowerAppContext.LightParameters.Find(id);

    public IEnumerable<LightParameters> Get(int[] ids) =>
        flowerAppContext.LightParameters.Where(lp => ids.Contains(lp.Id)).ToList();

    public async Task<bool> Create(LightParameters model)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();
        try
        {
            await flowerAppContext.LightParameters.AddAsync(model);
            var result = await flowerAppContext.SaveChangesAsync() > 0;
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> Update(LightParameters model)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();
        try
        {
            flowerAppContext.LightParameters.Update(model);
            var result = await flowerAppContext.SaveChangesAsync() > 0;
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();
        try
        {
            var lightParameter = await flowerAppContext.LightParameters.FindAsync(id);
            if (lightParameter == null) return false;

            flowerAppContext.LightParameters.Remove(lightParameter);
            var result = await flowerAppContext.SaveChangesAsync() > 0;
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}