using System.Threading.Tasks;
using Orleans;

namespace ChangFei.Interfaces
{
    public interface IWriteDataGrain:IGrainWithStringKey
    {
        Task StoreData();
    }
}
