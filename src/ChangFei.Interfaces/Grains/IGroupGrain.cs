using System.Threading.Tasks;
using Orleans;

namespace ChangFei.Interfaces.Grains
{
    public interface IGroupGrain:IGrainWithStringKey
    {
        Task SendChatMessageAsync(string userId);
    }
}
