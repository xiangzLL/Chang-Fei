using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChangFei.Client
{
    public class ChatClient
    {
        private HubConnection _hubConnection;

        public Task StartAsync(string url)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();
            return _hubConnection.StartAsync();
        }

    }
}
