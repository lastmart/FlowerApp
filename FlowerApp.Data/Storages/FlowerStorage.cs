using FlowerApp.Domain.DbModels;

namespace FlowerApp.Data.Storages;

public class FlowerStorage : IStorageBase<Flower, int>
{
    private readonly FlowerAppContext flowerAppContext;

    public FlowerStorage(FlowerAppContext flowerAppContext)
    {
        this.flowerAppContext = flowerAppContext;
    }

    public Flower? Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Flower> Get(int[] ids)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Create(Flower model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Flower model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}