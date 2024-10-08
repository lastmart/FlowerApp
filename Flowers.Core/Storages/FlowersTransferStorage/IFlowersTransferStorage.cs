using FlowersCareAPI.Models;

namespace FlowersCareAPI.Storages.FlowersTransferStorage;

public interface IFlowersTransferStorage
{
    void SendTransferRequest(string userIdm, Page<Flower> givingFlowers, Page<TransferFlower> acceptedFlowers);
}