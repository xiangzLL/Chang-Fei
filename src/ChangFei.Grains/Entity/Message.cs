namespace ChangFei.Grains.Entity
{
    public enum MessageType
    {
        Text,
        Image,
        File
    }

    public class BaseMessage
    {
        public MessageType MessageType { get; }

        public BaseMessage(MessageType messageType)
        {
            MessageType = messageType;
        }
    }

    public class TextMessage : BaseMessage
    {
        public string Content;

        public TextMessage(string content) : base(MessageType.Text)
        {
            Content = content;
        }
    }

    public class ImageMessage : BaseMessage
    {
        public byte[] ThumbnailImage { get; }

        public string OriginalImageUrl { get; }

        public ImageMessage(byte[] thumbnailImage,string originalImageUrl) : base(MessageType.Image)
        {
            ThumbnailImage = thumbnailImage;
            OriginalImageUrl = originalImageUrl;
        }
    }
}
