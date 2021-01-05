namespace ChangFei.Core.Message
{
    public class ImageMessage:Message
    {
        public ImageMessage(string userId, string targetId) : base(userId, targetId, MessageType.Image)
        {
        }
    }
}
