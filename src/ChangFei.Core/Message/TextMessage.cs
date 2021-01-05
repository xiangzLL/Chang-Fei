namespace ChangFei.Core.Message
{
    public class TextMessage : Message
    {
        public TextMessage(bool isGroup = false) : base(MessageType.Text, isGroup)
        {
        }
    }
}
