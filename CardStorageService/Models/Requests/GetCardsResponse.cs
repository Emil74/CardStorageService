namespace CardStorageService.Models.Requests
{
    public class GetCardsResponse : IOperationrResult
    {
        public IList<CardDto>? Cards { get; set; }
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
