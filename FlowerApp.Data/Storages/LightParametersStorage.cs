using FlowerApp.Domain.DbModels;

namespace FlowerApp.Data.Storages;

public class LightParametersStorage : IStorageBase<LightParameters, int>
{
    public LightParameters? Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<LightParameters> Get(int[] ids)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Create(LightParameters model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(LightParameters model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}