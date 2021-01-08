using System;
using System.Threading.Tasks;
using ChangFei.Core.Message;
using ChangFei.Grains.Repositories;

namespace ChangFei.Silo.Repositories
{
    public class IMDataRepositories:IIMDataRepository
    {
        public Task InsertData(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
