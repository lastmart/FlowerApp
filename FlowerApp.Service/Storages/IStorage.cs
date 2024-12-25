namespace FlowerApp.Service.Storages;

public interface IStorage<TAppModel, in TDbModelPk>
{
    public Task<TAppModel?> Get(TDbModelPk id);
    public Task<IList<TAppModel>> Get(TDbModelPk[] ids);
    public Task<bool> Create(TAppModel model);
    public Task<bool> Update(TAppModel model);
    public Task<bool> Delete(TDbModelPk id);
}