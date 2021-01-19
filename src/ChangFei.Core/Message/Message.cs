using System;
using ChangFei.Core.Utilities;

namespace ChangFei.Core.Message
{
    public enum MessageType
    {
        Text,
        Image,
        File
    }

    /// <summary>
    /// Message record
    /// </summary>
    public class Message
    {
        public string Id { get; }
        /// <summary>
        /// Sender user id
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Recipient user id
        /// </summary>
        public string Recipient { get; set; }

        public object MessageContent { get; set; }

        public string SendTime { get; set; }

        public bool IsGroup { get; set; }

        public Message(string sender,string recipient,bool isGroup=false)
        {
            Id = IdHelper.Generate<string>();
            SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Sender = sender;
            Recipient = recipient;
            IsGroup = isGroup;
        }

        /// <summary>
        /// Create a text message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <param name="content"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        public static Message CreateText(string sender, string recipient, string content, bool isGroup = false)
        {
            var message = new Message(sender, recipient, isGroup)
            {
                MessageContent = new TextMessageContent(content)
            };
            return message;
        }

        /// <summary>
        /// Create a image message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <param name="thumbnail"></param>
        /// <param name="originalUrl"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        public static Message CreateImage(string sender, string recipient, byte[] thumbnail,string originalUrl,bool isGroup = false)
        {
            var message = new Message(sender, recipient, isGroup)
            {
                MessageContent = new ImageMessageContent(thumbnail,originalUrl)
            };
            return message;
        }
    }

    public class BaseMessageContent
    {
        public MessageType MessageType { get; }

        public BaseMessageContent(MessageType messageType)
        {
            MessageType = messageType;
        }
    }

    public class TextMessageContent : BaseMessageContent
    {
        public string Content;

        public TextMessageContent(string content) : base(MessageType.Text)
        {
            Content = content;
        }
    }

    public class ImageMessageContent : BaseMessageContent
    {
        public byte[] Thumbnail;

        public string OriginalUrl;

        public ImageMessageContent(byte[] thumbnail, string originalUrl) : base(MessageType.Image)
        {
            Thumbnail = thumbnail;
            OriginalUrl = originalUrl;
        }
    }
}
