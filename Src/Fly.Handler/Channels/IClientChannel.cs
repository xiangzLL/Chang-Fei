using System.Net;
using System.Threading.Tasks;

namespace Fly.Handler.Channels
{
    public interface IClientChannel:IChannel
    {

        Task ConnectAsync(EndPoint remoteAddress);

        Task DisconnectAsync();
    }
}
