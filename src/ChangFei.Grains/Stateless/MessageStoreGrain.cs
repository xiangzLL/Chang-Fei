using System.Threading.Tasks;
using ChangFei.Core.Message;
using ChangFei.Grains.Repositories;
using ChangFei.Interfaces;
using Orleans;
using Orleans.Concurrency;

namespace ChangFei.Grains.Stateless
{
    [StatelessWorker]
    public class MessageStoreGrain : Grain,IMessageStoreGrain
    {
        private readonly IMessageRepository _dataRepository;

        public MessageStoreGrain(IMessageRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public Task StoreMessageAsync(Message message)
        {
            return _dataRepository.InsertAsync(message);
        }
    }
}
