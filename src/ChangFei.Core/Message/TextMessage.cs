namespace ChangFei.Core.Message
{
    public class TextMessage : Message
    {
        public TextMessage(string userId, string targetId) : base(userId, targetId, MessageType.Image)
        {
        }
    }
}
