using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Fly.Handler.Channels;

namespace Fly.Handler.Tcp
{
    /// <summary>
    /// Tcp监听通道
    /// </summary>
    public class TcpServerChannel:AbstractChannel,IServerChannel
    {
        private readonly ConcurrentDictionary<string,IClientChannel> _clientChannels = 
            new ConcurrentDictionary<string, IClientChannel>();

        private TcpListener _listener;
        private volatile bool _running;

        public TcpServerChannel() : base()
        {

        }

        public Task BindAsync(int port)
        {
            if (_listener == null)
            {

            }
            else
            {
                throw new InvalidOperationException("Server already start listen");
            }
        }

        public Task CloseAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
