using System.Threading.Tasks;
using ChangFei.Core.Message;
using Orleans;

namespace ChangFei.Interfaces
{
    public interface IMessageStoreGrain:IGrainWithIntegerKey
    {
        Task StoreMessageAsync(Message message);
    }
}
