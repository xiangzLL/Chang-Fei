using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Fly.Handler.Channels;

namespace Fly.Handler.Tcp
{
    /// <summary>
    /// Tcp 协议使用的客户端
    /// </summary>
    class FlyTcpClient :IClient
    {
        private readonly TcpClient _client;
        private BufferedStream _readStream;
        private BufferedStream _writeStream;
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

        public HostInfo Remote { get; private set; }
        public HostInfo Local { get; private set; }
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
                throw new ConnectionTimeoutException();
            }
            InitializeClient();
        }

        /// <summary>
        /// 服务端接收连接使用
        /// </summary>
        /// <param name="client">TcpClient</param>
        public FlyTcpClient(TcpClient client)
        {
            _client = client;
            InitializeClient();
        }

        private void InitializeClient()
        {
            var remoteEndPoint = (IPEndPoint)_client.Client.RemoteEndPoint;
            Remote = new HostInfo(remoteEndPoint.Address.ToString(), remoteEndPoint.Port, "Server");
            var localEndPoint = (IPEndPoint)_client.Client.LocalEndPoint;
            Local = new HostInfo(localEndPoint.Address.ToString(), localEndPoint.Port, "Client");
            _readStream = new BufferedStream(_client.GetStream());
            _writeStream = new BufferedStream(_client.GetStream());
            Connected = true;
        }

        public void Disconnect()
        {
            _client.Dispose();
            Connected = false;
        }
    }
}
