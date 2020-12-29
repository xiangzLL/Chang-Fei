namespace ChangFei.Core.Message
{
    /// <summary>
    /// Base message
    /// </summary>
    public abstract class Message
    {

        public string TargetUserId { get; }

        protected Message(MessageType messageType)
        {

        }
    }
}
