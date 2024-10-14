using FlowersCareAPI.Data;

namespace FlowersCareAPI.Storages.FlowersStorage;

public class FlowersStorage : IFlowersStorage
{
    private readonly FlowersContext flowersContext;

    public FlowersStorage(FlowersContext flowersContext)
    {
        this.flowersContext = flowersContext;
    }

    public IEnumerable<Flower> GetAll()
    {
        return flowersContext.Flowers.ToList();
    }

    public Flower? GetById(int fid)
    {
        return flowersContext.Flowers.Find(fid);
    }
}