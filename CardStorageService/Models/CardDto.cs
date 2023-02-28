namespace CardStorageService.Models
{
    public class CardDto
    {
        public string CardNo { get; set; }

        public string? Name { get; set; }

        public string? CVV2 { get; set; }

        public string ExpDate { get; set; }
    }
}
