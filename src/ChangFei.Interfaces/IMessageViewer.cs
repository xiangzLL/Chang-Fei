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
        /// Receive a user message
        /// </summary>
        /// <param name="message">User message</param>
        void ReceiveUserMessage(Message message);

        /// <summary>
        /// Receive a group message
        /// </summary>
        /// <param name="message">Group message</param>
        void ReceiveGroupMessage(Message message);
    }
}
