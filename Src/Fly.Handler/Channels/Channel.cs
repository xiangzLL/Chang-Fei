using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fly.Handler.Extensions;
using Fly.Handler.IO;

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
            using (new ReadTimeoutTranscation(this,timeout))
            {
                var headerData = _client.ReadBinary(8);
                if (headerData.Length != 8)
                {
                    throw new ErrorDataException("Head data length error.");
                }
                var versionData = new byte[2];
                var xyData = new byte[2];
                var zData = new short[1];
                Buffer.BlockCopy(headerData, 0, versionData, 0, versionData.Length);
                if (versionData[0] != MajorVersion)
                {
                    throw new ErrorDataException("Major version not match.");
                }
                Buffer.BlockCopy(headerData, 4, xyData, 0, xyData.Length);
                Buffer.BlockCopy(headerData, 6, zData, 0, zData.Length * sizeof(short));
                var x = xyData[0];
                var y = xyData[1];
                var z = zData[0];
                //校验数据
                if (z != x * 3 + y * 3 - (x - y))
                {
                    throw new ErrorDataException("Xyz data error.");
                }
                DataTransfered += 8;
            }
        }

        protected async Task ReadHeaderAsync(int timeout = 0)
        {
            using (new ReadTimeoutTranscation(this, timeout))
            {
                var headerData = await _client.ReadBinaryAsync(8).ConfigureAwait(false);
                if (headerData.Length != 8)
                {
                    throw new ErrorDataException("Head data length error.");
                }
                var versionData = new byte[2];
                var xyData = new byte[2];
                var zData = new short[1];
                Buffer.BlockCopy(headerData, 0, versionData, 0, versionData.Length);
                if (versionData[0] != MajorVersion)
                {
                    throw new ErrorDataException("Major version not match.");
                }

                Buffer.BlockCopy(headerData, 4, xyData, 0, xyData.Length);
                Buffer.BlockCopy(headerData, 6, zData, 0, zData.Length * sizeof(short));
                var x = xyData[0];
                var y = xyData[1];
                var z = zData[0];
                //校验数据
                if (z != x * 3 + y * 3 - (x - y))
                {
                    throw new ErrorDataException("Xyz data error.");
                }
                DataTransfered += 8;
            }
        }

        protected void WriteHeader(int timeout = 0)
        {
            using (new WriteTimeoutTranscation(this, timeout))
            {
                var headerData = new byte[8];
                var versionData = new[] { MajorVersion, MinorVersion };
                var x = (byte)Random.Next(byte.MaxValue);
                var y = (byte)Random.Next(byte.MaxValue);
                var z = (short)(x * 3 + y * 3 - (x - y));
                var xyData = new[] { x, y };
                var zData = new[] { z };
                Buffer.BlockCopy(versionData, 0, headerData, 0, versionData.Length);
                Buffer.BlockCopy(xyData, 0, headerData, 4, xyData.Length);
                Buffer.BlockCopy(zData, 0, headerData, 6, zData.Length * sizeof(short));
                //写头文件
                _client.WriteBinary(headerData);
                DataTransfered += 8;
            }
        }

        protected async Task WriteHeaderAsync(int timeout = 0)
        {
            using (new WriteTimeoutTranscation(this, timeout))
            {
                var headerData = new byte[8];
                var versionData = new[] { MajorVersion, MinorVersion };
                var x = (byte)Random.Next(byte.MaxValue);
                var y = (byte)Random.Next(byte.MaxValue);
                var z = (short)(x * 3 + y * 3 - (x - y));
                var xyData = new[] { x, y };
                var zData = new[] { z };
                Buffer.BlockCopy(versionData, 0, headerData, 0, versionData.Length);
                Buffer.BlockCopy(xyData, 0, headerData, 4, xyData.Length);
                Buffer.BlockCopy(zData, 0, headerData, 6, zData.Length * sizeof(short));
                //写头文件
                await _client.WriteBinaryAsync(headerData).ConfigureAwait(false);
                DataTransfered += 8;
            }
        }

        protected IBuffer ReadBuffer(int timeout=0)
        {
            using (new ReadTimeoutTranscation(this, timeout))
            {
                var buffer = _client.ReadBuffer();
                DataTransfered += buffer.Size;
                return buffer;
            }
        }

        protected async Task<IBuffer> ReadBufferAsync(int timeout = 0)
        {
            using (new ReadTimeoutTranscation(this, timeout))
            {
                var buffer = await _client.ReadBufferAsync().ConfigureAwait(false);
                DataTransfered += buffer.Size;
                return buffer;
            }
        }

        protected void WriteBuffer(IBuffer buffer, int timeout = 0)
        {
            using (new WriteTimeoutTranscation(this, timeout))
            {
                _client.WriteBuffer(buffer);
                DataTransfered += buffer.Size;
            }
        }

        protected async Task WriteBufferAsync(IBuffer buffer, int timeout = 0)
        {
            using (new WriteTimeoutTranscation(this, timeout))
            {
                await _client.WriteBufferAsync(buffer).ConfigureAwait(false);
                DataTransfered += buffer.Size;
            }
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
