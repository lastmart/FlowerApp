namespace FlowerApp.Domain.Models.Operation;

public class FailureOperationStatus: IRepositoryOperationStatus
{
    public string Code { get; set; }
    public string Message { get; set; }
}