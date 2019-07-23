using System.Threading.Tasks;
using StackExchange.Redis;

namespace Route.Infrastructure.Repositories
{
    public class RedisIMRepository:IIMRepository
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisIMRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public Task UpdateUserAddressAsync(int userId, string ipAddress)
        {
            //_database.StringSetAsync()
            throw new System.NotImplementedException();

        }

        public Task<string> GetUserAddressAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
