using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using ChangFei.Core.Message;
using ChangFei.Interfaces;
using ChangFei.Interfaces.Grains;
using Orleans;

namespace ChangFei.Grains.Grains
{
    public class UserState
    {
        public bool Logined { get; set; }

        public IMessageViewer Viewer { get; set; }

        public Queue<Message> ReceivedMessages { get; set; }
    }

    public class UserGrain: Grain<UserState>, IUserGrain
    {
        private IMessageViewer _messageViewer;

        public string UserId => this.GetPrimaryKeyString();

        #region Grain overrides

        public override Task OnActivateAsync()
        {
            if (State.ReceivedMessages == null)
            {
                State.ReceivedMessages = new Queue<Message>();
            }
            return base.OnActivateAsync();
        }

        #endregion

        public Task SendMessageAsync(Message message)
        {
            //send message to target user
            var targetGrain = GrainFactory.GetGrain<IUserGrain>(message.TargetId);
            return targetGrain.NewMessageAsync(message);
        }

        public Task<ImmutableList<Message>> GetOfflineMessages()
        {
            return Task.FromResult(State.ReceivedMessages.ToImmutableList());
        }

        public async Task NewMessageAsync(Message message)
        {
            if (State.Logined)
            {
                //if user is online, call message observer.
                _messageViewer.ReceiveUserMessage(Message.ConvertToResponseMessage(message));
                //store transfer message to db
            }
            else
            {
                //if user is not offline, Store message to queue.
                State.ReceivedMessages.Enqueue(message);
                await WriteStateAsync();
            }
        }

        public async Task LoginAsync(IMessageViewer viewer)
        {
            State.Logined = true;
            await WriteStateAsync();
            _messageViewer = viewer;
            Console.WriteLine($"{UserId} login success.");
        }

        public async Task LogoutAsync()
        {
            State.Logined = false;
            await WriteStateAsync();
            _messageViewer = null;
            Console.WriteLine($"{UserId} logout success");
        }
    }
}
