using System;

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
        /// System message
        /// </summary>
        System
    }

    /// <summary>
    /// Base message
    /// </summary>
    public abstract class Message
    {
        public string UserId { get; private set; }

        public string TargetId { get; private set; }

        public MessageType MessageType { get; }

        public string Content { get; protected set; }

        public string SendTime { get; private set; }

        public string ReceiveTime { get; private set; }

        protected Message(string userId,string targetId, MessageType messageType)
        {
            UserId = userId;
            TargetId = targetId;
            MessageType = messageType;
            SendTime = DateTime.Now.ToString("yyyy-mm-dd hh:MM:ss");
        }

        /// <summary>
        /// Convert message to response message
        /// </summary>
        /// <param name="originalMessage">original message</param>
        /// <returns>Response message</returns>
        public static Message ConvertToResponseMessage(Message originalMessage)
        {
            var newUserId = originalMessage.TargetId;
            originalMessage.TargetId = originalMessage.UserId;
            originalMessage.UserId = newUserId;
            return originalMessage;
        }

    }
}
