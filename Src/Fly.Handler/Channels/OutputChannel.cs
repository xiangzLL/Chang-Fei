using Fly.Handler.IO;

namespace Fly.Handler.Channels
{
    public sealed class OutputChannel:Channel
    {
        private readonly object _sendLock = new object();

        public OutputChannel(int channelId, IClient client) : base(channelId, client)
        {
        }

        internal IBuffer Send()
    }
}
