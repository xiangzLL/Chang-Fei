using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChangFei.Client.Client
{
    public class MessageClient : IMessageClient
    {
        private readonly HubConnection _hubConnection;

        public MessageClient(string url)
        {
            _hubConnection.On("NewMessage", () =>
            {

            });
            _hubConnection.On("FriendRequest", () =>
            {

            });
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();
        }

        public Task SendMessage(string userId)
        {
            return _hubConnection.InvokeAsync("SendMessage");
        }

        public Task RequestFriend(string userId)
        {
            return Task.CompletedTask;
        }

        public Task ConnectAsync(string url)
        {
            
            return _hubConnection.StartAsync();
        }
    }
}
