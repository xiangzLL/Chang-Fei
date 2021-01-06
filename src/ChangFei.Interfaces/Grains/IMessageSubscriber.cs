using System.Threading.Tasks;
using ChangFei.Core.Message;
using Orleans;

namespace ChangFei.Interfaces.Grains
{
    public interface IMessageSubscriber:IGrainWithStringKey
    {
        Task NewMessageAsync(Message message);
    }

    public interface IGroupMessageSubscriber : IGrainWithStringKey
    {
        Task NewGroupMessageAsync(Message message);
    }
}
