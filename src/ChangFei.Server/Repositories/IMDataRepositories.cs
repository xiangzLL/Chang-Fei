using System;
using System.Threading.Tasks;
using ChangFei.Grains.Repositories;

namespace ChangFei.Silo.Repositories
{
    public class IMDataRepositories<T>:IIMDataRepository<T>
    {
        public Task InsertData<T>(T message)
        {
            throw new NotImplementedException();
        }
    }
}
