namespace CardStorageService.Models.Requests
{
    public class CreateClientResponse : IOperationrResult
    {
        public int ClientId { get; set; }
        public int ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
