using System.Collections.Immutable;
using System.Threading.Tasks;
using ChangFei.Core.Message;
using Orleans;

namespace ChangFei.Interfaces.Grains
{
    public interface IMessageSender:IGrainWithStringKey
    {
        Task SendMessageAsync(Message message);

        Task<ImmutableList<Message>> GetOfflineMessages();
    }

    public interface IGroupMessageSender : IGrainWithStringKey
    {
        Task SendGroupMessageAsync(Message message);
    }
}
