using FlowersCareAPI.Models;

namespace FlowersCareAPI.Storages.FlowersTransferStorage;

public class FlowersTransferStorage : IFlowersTransferStorage
{
    private static Dictionary<string, Flower> givingFlowers;
    private static Dictionary<string, TransferFlower> acceptingFlowers;

    public void SendTransferRequest(string userId, Page<Flower> givingFlowers, Page<TransferFlower> acceptingFlowers)
    {
        throw new NotImplementedException();
    }
}