using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Fly.Handler.Channels;

namespace Fly.Handler.Tcp
{
    //开启Tcp监听
    public class TcpServerChannel:IServerChannel
    {
        private TcpListener _listener;
        private volatile bool _running;

        public int Id { get; }
        public bool IsClosed { get; set; }
        public HostInfo Remote { get; }
        public HostInfo Local { get; }

        public TcpServerChannel()
        {

        }

        public Task BindAsync(int port)
        {
            throw new System.NotImplementedException();
        }

        public Task CloseAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
