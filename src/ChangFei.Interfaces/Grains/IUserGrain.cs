using System.Threading.Tasks;
using ChangFei.Core.Message;
using Orleans;

namespace ChangFei.Interfaces.Grains
{
    /// <summary>
    /// User Grain
    /// </summary>
    public interface IUserGrain: IGrainWithStringKey
    {
        /// <summary>
        /// User send message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessageAsync(Message message);

        Task ReceiveMessageAsync(Message message);

        Task LoginAsync(IMessageViewer viewer);

        Task GetOfflineMessages();


    }
}
