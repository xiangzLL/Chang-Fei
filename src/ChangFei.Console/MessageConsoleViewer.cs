using ChangFei.Core.Message;
using ChangFei.Interfaces;

namespace ChangFei.Console
{
    /// <summary>
    /// Implements an <see cref="IMessageViewer"/> that outputs notifications to the console.
    /// </summary>
    public class MessageConsoleViewer:IMessageViewer
    {
        public void ReceiveUserMessage(Message message)
        {
            System.Console.WriteLine($"User {message.UserId}: {message.Content}");
        }

        public void ReceiveGroupMessage(Message message)
        {
            System.Console.WriteLine($"Group {message.UserId}: {message.MessageType}");
        }
    }
}
