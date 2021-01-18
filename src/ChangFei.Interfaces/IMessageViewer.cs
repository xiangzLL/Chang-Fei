using ChangFei.Core.Message;
using Orleans;

namespace ChangFei.Interfaces
{
    /// <summary>
    /// Interface of observers of an <see cref="IMessageViewer"/> instance.
    /// </summary>
    public interface IMessageViewer:IGrainObserver
    {
        /// <summary>
        /// Received a new message
        /// </summary>
        /// <param name="message">Message</param>
        void NewMessageAsync(Message message);
    }
}
