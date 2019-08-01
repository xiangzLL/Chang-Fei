using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Fly.Handler.Channels;
using Fly.Handler.IO;

namespace Fly.Handler.Tcp
{
    /// <summary>
    /// Tcp监听通道
    /// </summary>
    public class TcpServerChannel:AbstractChannel,IServerChannel
    {
        private readonly ConcurrentBag<ChannelContext> _channelContexts = 
            new ConcurrentBag<ChannelContext>();

        private TcpListener _listener;
        private volatile bool _running;
        private AbstractClientStatusHandler _statusHandler;

        public bool Runnning
        {
            get => _running;
            private set
            {
                if (_running != value)
                {
                    _running = value;
                }
            }
        }
        bool IChannel.IsClosed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TcpServerChannel() : base()
        {

        }

        public async Task BindAsync(int port)
        {
            if (_listener == null)
            {
                var serverAddress = IPAddress.Any;
                var endPoint = new IPEndPoint(serverAddress,port);
                _listener = new TcpListener(endPoint);
                _listener.Start(Environment.ProcessorCount*256);
                Runnning = true;
                while (Runnning)
                {
                    try
                    {
                        var client = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);
                        HandleAccept(client);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Server already start listen");
            }
        }

        /// <summary>
        /// 接收TcpClient
        /// </summary>
        /// <param name="tcpClient"></param>
        /// <returns></returns>
        private void HandleAccept(TcpClient tcpClient)
        {
            var flyClient = new FlyTcpClient(tcpClient);
            var inputChannel =new InputChannel(flyClient.Id,flyClient);
            inputChannel.BufferReceived += OnBufferReceived;
            inputChannel.Closed += OnChannelClosed;
            var outputChannel = new OutputChannel(1,flyClient);
        }

        private void OnChannelClosed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnBufferReceived(object sender, BufferEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Task CloseAsync()
        {
            throw new System.NotImplementedException();
        }

        public void AddStatusHandler(AbstractClientStatusHandler handler)
        {
            _statusHandler = handler;
        }

        public Task WriteAsync(IBuffer buffer)
        {
            throw new NotImplementedException();
        }
    }
}
