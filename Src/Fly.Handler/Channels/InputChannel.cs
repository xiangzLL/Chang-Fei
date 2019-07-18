namespace Fly.Handler.Channels
{
    public sealed class InputChannel:Channel
    {
        public InputChannel(int channelId, IClient client) : base(channelId, client)
        {
        }
    }
}
