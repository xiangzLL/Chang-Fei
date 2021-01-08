using System;
using System.Threading.Tasks;
using ChangFei.Grains.Repositories;
using ChangFei.Interfaces;
using Orleans.Concurrency;

namespace ChangFei.Grains.Stateless
{
    [StatelessWorker]
    public class WriteDataGrain : Orleans.Grain, IWriteDataGrain
    {
        private readonly IIMDataRepository _dataRepository;

        public WriteDataGrain(IIMDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public Task StoreData()
        {
            throw new NotImplementedException();
        }
    }
}
