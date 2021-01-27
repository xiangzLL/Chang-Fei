using System.Threading.Tasks;

namespace ChangFei.Client.Client
{
    public interface IMessageClient
    {
        Task ConnectAsync(string url);

        Task SendMessage(string userId);


    }
}
