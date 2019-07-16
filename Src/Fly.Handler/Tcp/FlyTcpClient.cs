using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Fly.Handler.Channels;

namespace Fly.Handler.Tcp
{
    /// <summary>
    /// 封装的TcpClient
    /// </summary>
    public class FlyTcpClient :IClient
    {
        private readonly TcpClient _client;
        private readonly BufferedStream _readStream;
        private readonly BufferedStream _writeStream;
        private int _sendTimeout;
        private int _readTimeout;

        public int SendTimeout
        {
            get => _sendTimeout;
            set
            {
                if (_sendTimeout != value)
                {
                    _sendTimeout = value;
                    _client.SendTimeout = _sendTimeout;
                }
            }
        }

        public int ReadTimeout
        {
            get => _readTimeout;
            set
            {
                if (_readTimeout != value)
                {
                    _readTimeout = value;
                    _client.ReceiveTimeout = _readTimeout;
                }
            }
        }

        public HostInfo Remote { get; }
        public HostInfo Local { get; }
        public Stream ReadStream => _readStream;
        public Stream WriteStream => _writeStream;
        public bool Connected { get; private set; }

        /// <summary>
        /// 客户端使用
        /// </summary>
        /// <param name="ipEndPoint">连接的服务端ip地址</param>
        public FlyTcpClient(IPEndPoint ipEndPoint)
        {
            _client = new TcpClient(ipEndPoint.AddressFamily);
            _client.ConnectAsync(ipEndPoint.Address, ipEndPoint.Port).Wait(TimeSpan.FromSeconds(5));
            if (!_client.Connected)
            {
                _client.Dispose();

            }
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
