using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChangFei.Core.Message;
using ChangFei.Interfaces.Grains;
using Microsoft.AspNetCore.Authorization;
using Orleans;

namespace ChangFei.Route.Hub
{
    [Authorize]
    public class MessageHub:Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IClusterClient _client;
        private readonly ConcurrentDictionary<string,string> _userDic = new ConcurrentDictionary<string, string>();

        public MessageHub(IClusterClient client)
        {
            _client = client;
        }

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            //userGrain login success

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            //userGrain logout
            return base.OnDisconnectedAsync(exception);
        }

        public Task SendMessageAsync(Message message)
        {
            //same server, direct send
            //if (_onlineUsers.Contains(message.Recipient))
            //{
            //    //send to recipient.

            //    return Task.CompletedTask;
            //}

            var userGrain = _client.GetGrain<IUserGrain>(message.Recipient);
            return userGrain.NewMessageAsync(message);
        }

        public Task RequestFriend(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
