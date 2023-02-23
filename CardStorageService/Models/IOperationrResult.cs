namespace CardStorageService.Models
{
    public interface IOperationrResult
    {
        int ErrorCode { get; }

        string? ErrorMessage { get; }
    }
}
