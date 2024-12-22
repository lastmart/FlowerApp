namespace FlowerApp.Service.Storages;

public interface IStorage<TDbModel, in TDbModelPk>
{
    public Task<TDbModel?> Get(TDbModelPk id);
    public Task<IList<TDbModel>> Get(TDbModelPk[] ids);
    public Task<bool> Create(TDbModel model);
    public Task<bool> Update(TDbModel model);
    public Task<bool> Delete(TDbModelPk id);
}