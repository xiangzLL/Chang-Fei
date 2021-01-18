using System;
using System.Threading.Tasks;
using ChangFei.Grains.Repositories;
using ChangFei.Interfaces;
using Orleans.Concurrency;

namespace ChangFei.Grains.Stateless
{
    [StatelessWorker]
    public class MessageStoreGrain : Orleans.Grain, IMessageStoreGrain
    {
        private readonly IMessageRepository _dataRepository;

        public MessageStoreGrain(IMessageRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public Task StoreMessage()
        {
            throw new NotImplementedException();
        }
    }
}
