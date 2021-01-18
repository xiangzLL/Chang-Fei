using System.Threading.Tasks;
using ChangFei.Grains.Entity;
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

        public Task StoreMessageAsync()
        {
            var messageRecord = new MessageRecord("111")
            {

            };
            return _dataRepository.InsertAsync(messageRecord);
        }
    }
}
