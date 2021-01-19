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
        public void NewMessageAsync(Message message)
        {
            var originalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"{message.Sender}: {((TextMessageContent)message.MessageContent).Content}");
            System.Console.ForegroundColor = originalColor;
        }
    }
}
