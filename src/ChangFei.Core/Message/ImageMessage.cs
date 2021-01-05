namespace ChangFei.Core.Message
{
    public class ImageMessage:Message
    {
        public ImageMessage(bool isGroup = false) : base(MessageType.Image, isGroup)
        {
        }
    }
}
