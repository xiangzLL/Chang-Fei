using System.Threading.Tasks;
using ChangFei.Core.Message;
using Orleans;

namespace ChangFei.Interfaces
{
    /// <summary>
    /// User Grain
    /// </summary>
    public interface IUserGrain: IGrainWithStringKey
    {
        /// <summary>
        /// Change User avatar
        /// </summary>
        /// <returns></returns>
        Task ChangeAvatarAsync();

        /// <summary>
        /// Request friend 
        /// </summary>
        /// <returns></returns>
        Task RequestFriendAsync();

        /// <summary>
        /// User send message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessageAsync(Message message);
    }
}
