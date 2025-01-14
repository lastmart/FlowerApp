namespace FlowerApp.Domain.Models.Operation;

public class SuccessOperationStatus<T> : IRepositoryOperationStatus
{
    public string Code { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
}