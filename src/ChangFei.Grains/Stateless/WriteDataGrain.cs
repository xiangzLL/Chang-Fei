using System;
using System.Threading.Tasks;
using ChangFei.Interfaces;
using Orleans.Concurrency;

namespace ChangFei.Grains.Stateless
{
    [StatelessWorker]
    public class WriteDataGrain : Orleans.Grain, IWriteDataGrain
    {
        public Task StoreData()
        {
            throw new NotImplementedException();
        }
    }
}
