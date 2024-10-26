namespace FlowerApp.Data.Storages;

public interface IStorageBase<TDbModel, in TDbModelPk>
{
    public TDbModel? Get(TDbModelPk id);
    public IEnumerable<TDbModel> Get(TDbModelPk[] ids);
    public Task<bool> Create(TDbModel model);
    public Task<bool> Update(TDbModel model);
    public Task<bool> Delete(TDbModelPk id);
}