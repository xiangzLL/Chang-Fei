using ChangFei.Core.Message;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChangFei.Silo
{
    public class MessageDataContext
    {
        private readonly IMongoDatabase _database;

        public MessageDataContext(IOptions<PersistenceOptions> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Message> Messages => _database.GetCollection<Message>("Message");
    }
}
