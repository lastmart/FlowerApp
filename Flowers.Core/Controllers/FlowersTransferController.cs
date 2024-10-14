using FlowersCareAPI.Models;
using FlowersCareAPI.Storages.FlowersTransferStorage;
using Microsoft.AspNetCore.Mvc;

namespace FlowersCareAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlowerTransfersController : ControllerBase
{
    private readonly IFlowersTransferStorage flowersTransferStorage;

    public FlowerTransfersController(IFlowersTransferStorage flowersTransferStorage)
    {
        this.flowersTransferStorage = flowersTransferStorage;
    }

    // [HttpPost("transfer/{userId}")]
    // public ActionResult<Flower> GetFlowerByScientificName(string userId, [FromBody] Page<Flower> givingFlowers,
    //     [FromBody] Page<TransferFlower> acceptingFlowers)
    // {
    //     flowersTransferStorage.SendTransferRequest(userId, givingFlowers, acceptingFlowers);
    //     return Ok();
    // }
}