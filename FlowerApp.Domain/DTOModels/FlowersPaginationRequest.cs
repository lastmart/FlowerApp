namespace FlowerApp.Domain.DTOModels;

public class FlowersPaginationRequest
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 50;
}