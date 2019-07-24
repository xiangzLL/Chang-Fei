using System.Threading.Tasks;

namespace Fly.Handler.Channels
{
    public interface IServerChannel:IChannel
    {
        Task BindAsync(int port);

        Task CloseAsync();

    }
}
