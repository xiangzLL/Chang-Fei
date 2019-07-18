using System;
using System.Threading.Tasks;
using Fly.Handler.IO;

namespace Fly.Handler.Channels
{
    public sealed class InputChannel:Channel
    {
        internal event EventHandler<BufferEventArgs> BufferReceived;

        public InputChannel(int channelId, IClient client) : base(channelId, client)
        {
        }

        internal void StartReceive()
        {
            Receive();
        }

        internal async Task StartReceiveAsync()
        {
            while (!IsClosed)
            {
                await ReceiveAsync().ConfigureAwait(false);
            }
        }

        private void Receive()
        {
            Task.Run(() =>
            {
                while (!IsClosed)
                {
                    try
                    {
                        var buffer = DoReceive();
                        OnBufferReceived(buffer);
                    }
                    catch (Exception ex)
                    {
                        var convertedException = NetworkException.Convert(ex);
                        if (!(convertedException is ConnectionTimeoutException))
                        {
                            Close();
                        }
                    }
                }
            });
        }

        private async Task ReceiveAsync()
        {
            try
            {
                var buffer = await DoReceiveAsync().ConfigureAwait(false);
                OnBufferReceived(buffer);
            }
            catch (Exception ex)
            {
                var convertedException = NetworkException.Convert(ex);
                if (!(convertedException is ConnectionTimeoutException))
                {
                    Close();
                }
            }
        }

        private IBuffer DoReceive()
        {
            ReadHeader();
            return ReadBuffer();
        }

        private async Task<IBuffer> DoReceiveAsync()
        {
            await ReadHeaderAsync().ConfigureAwait(false);
            return await ReadBufferAsync().ConfigureAwait(false);
        }

        internal void Reply(IBuffer buffer)
        {
            WriteHeader();
            WriteBuffer(buffer);
        }

        internal async Task ReplyAsync(IBuffer buffer)
        {
            await WriteHeaderAsync().ConfigureAwait(false);
            await WriteBufferAsync(buffer).ConfigureAwait(false);
        }

        private void OnBufferReceived(IBuffer buffer)
        {
            BufferReceived?.Invoke(this,new BufferEventArgs(buffer));
        }

        public override string ToString()
        {
            return $"InputChannel-{base.ToString()}";
        }
    }
}
