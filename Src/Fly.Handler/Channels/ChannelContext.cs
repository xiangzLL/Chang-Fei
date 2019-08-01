namespace Fly.Handler.Channels
{
    public class ChannelContext
    {
        public int Id { get; }

        public InputChannel InputChannel { get; }

        public OutputChannel OutputChannel { get; }

        public ChannelContext(InputChannel inputChannel, OutputChannel outputChannel)
        {
            InputChannel = inputChannel;
            OutputChannel = outputChannel;
        }
    }
}
