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
        await flowerAppContext.LightParameters.AddAsync(model);
        return await flowerAppContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(LightParameters model)
    {
        flowerAppContext.LightParameters.Update(model);
        return await flowerAppContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        var lightParameter = await flowerAppContext.LightParameters.FindAsync(id);
        if (lightParameter == null) return false;

        flowerAppContext.LightParameters.Remove(lightParameter);
        return await flowerAppContext.SaveChangesAsync() > 0;
    }
}