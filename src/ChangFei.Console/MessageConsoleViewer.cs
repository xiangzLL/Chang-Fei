using System;
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
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"{message.TargetId}: {message.Content}");
        }

        public void ReceiveGroupMessage(Message message)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"{message.TargetId}: {message.Content}");
        }
    }
}
