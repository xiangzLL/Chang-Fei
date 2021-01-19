using ChangFei.Core.Message;

namespace ChangFei.Core.Utilities
{
    public static class MessageExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <param name="content"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        public static Message.Message CreateTextMessage(this Message.Message message, string sender, string recipient, string content, bool isGroup = false)
        {
            var textMessage = new Message.Message(sender, recipient, isGroup)
            {
                MessageContent = new TextMessageContent(content)
            };
            return textMessage;
        }
    }
}
