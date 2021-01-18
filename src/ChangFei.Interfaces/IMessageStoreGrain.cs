using System.Threading.Tasks;
using Orleans;

namespace ChangFei.Interfaces
{
    public interface IMessageStoreGrain:IGrainWithIntegerKey
    {
        Task StoreMessageAsync();
    }
}
