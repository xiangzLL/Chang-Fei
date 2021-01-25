using System;
using System.Threading.Tasks;
using ChangFei.Core.Message;

namespace ChangFei.Route.Hub
{
    public class MessageHub:Microsoft.AspNetCore.SignalR.Hub
    {
        public override Task OnConnectedAsync()
        {
            
            var connectionId = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public Task SendMessageAsync(Message message)
        {
            return Task.CompletedTask;
        }

        public Task RequestFriend(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
