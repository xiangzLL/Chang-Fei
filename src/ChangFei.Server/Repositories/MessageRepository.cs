using System.Threading.Tasks;
using ChangFei.Grains.Entity;
using ChangFei.Grains.Repositories;
using Microsoft.Extensions.Options;

namespace ChangFei.Silo.Repositories
{
    public class MessageRepository:IMessageRepository
    {
        private readonly MessageDataContext _context;

        public MessageRepository(IOptions<PersistenceOptions> settings)
        {
            _context = new MessageDataContext(settings);
        }

        public Task InsertAsync(MessageRecord messageRecord)
        {
            return _context.MessageRecords.InsertOneAsync(messageRecord);
        }
    }
}
