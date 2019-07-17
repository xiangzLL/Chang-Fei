using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fly.Handler.Channels
{
    public class Channel
    {
        private static readonly Random Random = new Random();
        private readonly IClient _client;

        private int _readTimeoutTranscationLevel;
        private int _writeTimeoutTranscationLevel;

        private const byte MajorVersion = 2;
        private const byte MinorVersion = 1;

        /// <summary>
        /// 当前通道id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 通道是否关闭了
        /// </summary>
        public bool IsClosed { get; protected set; }

        public event EventHandler Closed;
        /// <summary>
        /// 远端信息，对server来说，这个意味着客户端，对client来说，这个意味是服务端
        /// </summary>
        public HostInfo Remote { get; }
        /// <summary>
        /// 本端信息，对server来着，这个意味着服务端，对client来说，这个意味着客户端
        /// </summary>
        public HostInfo Local { get; }

        /// <summary>
        /// 发送和接受的总数据量
        /// </summary>
        public long DataTransfered { get; private set; }

        public Channel(int channelId, IClient client)
        {
            Id = channelId;
            _client = client;
            Remote = _client.Remote;
            Local = _client.Local;
        }

        internal void UpdateChannelId(int channelId)
        {
            Id = channelId;
        }

        protected void ReadHeader(int timeout = 0)
        {

        }

        protected async Task ReadHeaderAsync(int timeout = 0)
        {

        }

        protected void WriteHeader(int timeout = 0)
        {

        }

        protected async Task WriteHeaderAsync(int timeout = 0)
        {

        }

        public void Close()
        {
            _client.Disconnect();
            IsClosed = true;
            OnClosed();
        }

        protected virtual void OnClosed()
        {
            Closed?.Invoke(this,EventArgs.Empty);
        }

        public override string ToString()
        {
            return $"{Remote}<->{Local}";
        }

        private class ReadTimeoutTranscation : IDisposable
        {
            private readonly Channel _channel;

            public ReadTimeoutTranscation(Channel channel, int timeout)
            {
                _channel = channel;
                if (_channel._readTimeoutTranscationLevel == 0)
                {
                    try
                    {
                        _channel._client.ReadTimeout = timeout;
                    }
                    catch
                    {
                        //ignore
                    }
                }

                _channel._readTimeoutTranscationLevel++;
            }

            public void Dispose()
            {
                _channel._readTimeoutTranscationLevel--;
                if (_channel._readTimeoutTranscationLevel == 0)
                {
                    try
                    {
                        _channel._client.ReadTimeout = 0;
                    }
                    catch
                    {
                        //ignore
                    }
                }
            }
        }

        private class WriteTimeoutTranscation : IDisposable
        {
            private readonly Channel _channel;

            public WriteTimeoutTranscation(Channel channel, int timeout)
            {
                _channel = channel;
                if (_channel._writeTimeoutTranscationLevel == 0)
                {
                    try
                    {
                        _channel._client.SendTimeout = timeout;
                    }
                    catch
                    {
                        //ignore
                    }
                }
                _channel._writeTimeoutTranscationLevel++;
            }

            public void Dispose()
            {
                _channel._writeTimeoutTranscationLevel--;
                if (_channel._writeTimeoutTranscationLevel == 0)
                {
                    try
                    {
                        _channel._client.SendTimeout = 0;
                    }
                    catch
                    {
                        //ignore
                    }
                }
            }
        }
    }
}
