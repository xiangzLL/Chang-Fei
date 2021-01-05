namespace ChangFei.Core.Message
{
    public enum MessageType
    {
        /// <summary>
        /// Text
        /// </summary>
        Text,
        /// <summary>
        /// Image
        /// </summary>
        Image,
        /// <summary>
        /// Video request
        /// </summary>
        Video,
        /// <summary>
        /// Audio request
        /// </summary>
        Audio,
        /// <summary>
        /// Request add friend
        /// </summary>
        RequestFriend,
    }

    /// <summary>
    /// Base message
    /// </summary>
    public abstract class Message
    {
        public string TargetId { get; }

        public MessageType MessageType { get; }

        public bool IsGroup { get; }

        protected Message(MessageType messageType,bool isGroup)
        {
            MessageType = messageType;
            IsGroup = isGroup;
        }
    }
}
