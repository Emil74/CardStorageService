using CardStorageService.Data;
using CardStorageService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Security.AccessControl;

namespace CardStorageService.Services.Impl
{
    public class CardRepository : ICardRepositoryService
    {
        #region Services
        private readonly CardStorageServiceDbContext _context;
        private readonly IOptions<DatabaseOptions> _databaseOptions;
        private readonly ILogger<ClientRepository> _logger;

        #endregion

        #region Constructors
        public CardRepository(ILogger<ClientRepository> loger, CardStorageServiceDbContext context,
            IOptions<DatabaseOptions> databaseOptions)
        {
            _logger = loger;
            _context = context;
            _databaseOptions = databaseOptions;
        }

        public string Create(Card data)
        {
            var client = _context.Clients.FirstOrDefault(client => client.ClientId == data.ClientId);
            if (client == null)
                throw new Exception("Client not found.");

            _context.Cards.Add(data);
            _context.SaveChanges();
            return data.CardId.ToString();
        }

        public int Delete(string data)
        {
            throw new NotImplementedException();
        }

        public IList<Card> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Card> GetByClientId(string id)
        {
            List<Card> cards = new List<Card>();
            using (SqlConnection sqlConnection = new SqlConnection(_databaseOptions.Value.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(String.Format("select*from cards where ClientId = {0}", id), sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        cards.Add(new Card
                        {
                            CardId = new Guid(reader["CardId"].ToString()),
                            CardNo = reader["CardNo"]?.ToString(),
                            Name = reader["Name"]?.ToString(),
                            CVV2 = reader["CVV2"]?.ToString(),
                            ExpDate = Convert.ToDateTime(reader["ExpDate"])
                        });
                    }
                }
            }
            return cards;

            // return _context.Cards.Where(card => card.ClientId == id).ToList();
        }

        public Card GetById(string id)
        {
            throw new NotImplementedException();
        }

        public int Update(Card data)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
