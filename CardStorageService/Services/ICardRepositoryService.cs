using CardStorageService.Data;

namespace CardStorageService.Services
{
    public interface ICardRepositoryService : IRepository<Card, string>
    {
        IList<Card> GetByClientId(string clientId);
    }
}
