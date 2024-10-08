using FlowersCareAPI.Models;

namespace FlowersCareAPI.Storages.FlowersStorage;

public interface IFlowersStorage
{
    public IEnumerable<Flower> GetAll();
    public Flower? GetByScientificName(string scientificName);
}