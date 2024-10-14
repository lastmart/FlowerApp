namespace FlowersCareAPI.Storages.FlowersStorage;

public interface IFlowersStorage
{
    public IEnumerable<Flower> GetAll();
    public Flower? GetById(int fid);
}