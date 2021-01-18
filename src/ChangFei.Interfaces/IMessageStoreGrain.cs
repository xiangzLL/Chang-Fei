using System.Threading.Tasks;
using Orleans;

namespace ChangFei.Interfaces
{
    public interface IMessageStoreGrain:IGrainWithStringKey
    {
        Task StoreMessage();
    }
}
