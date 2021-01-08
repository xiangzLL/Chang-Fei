namespace ChangFei.Core.Message
{
    public class TextMessage : Message
    {
        public TextMessage(string userId, string targetId, string content) : base(userId, targetId, MessageType.Text)
        {
            Content = content;
        }
    }
}
